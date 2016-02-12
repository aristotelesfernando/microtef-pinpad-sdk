using System.Linq;
using System.Collections.Generic;
using System;
using Pinpad.Core.TypeCode;
using System.IO;
using Pinpad.Core.Events;
using Pinpad.Core.Commands;
using Pinpad.Core.Utilities;
using MicroPos.CrossPlatform;

namespace Pinpad.Core.Pinpad
{
	/// <summary>
	/// PinPad storage adapter
	/// </summary>
	public class PinpadStorage 
	{
		// Members
		/// <summary>
		/// Pinpad communication adapter
		/// </summary>
		private PinpadCommunication communication { get; set; }
		private Dictionary<string, string> fileNameToPathDictionary { get; set; }
		/// <summary>
		/// Gets a new array containing the File Names used
		/// </summary>
		public string[] FileNameCollection {
			get {
				lock (this.fileNameToPathDictionary) {
					return this.fileNameToPathDictionary.Keys.ToArray();
				}
			}
		}
		/// <summary>
		/// Are all the files loaded into the PinPad?
		/// </summary>
		public bool AreAllFilesUpToDate { get; private set; }
		/// <summary>
		/// Event called when a file is fully loaded into the PinPad
		/// </summary>
		public event EventHandler<PinpadFileLoadedEventArgs> OnFileLoaded;

		// Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="pinPad">PinPad to use</param>
		public PinpadStorage(PinpadCommunication communication)
		{
			this.communication = communication;
			this.fileNameToPathDictionary = new Dictionary<string, string>();
			this.AreAllFilesUpToDate = true;
		}

		// Methods
		/// <summary>
		/// Adds a file to the collection
		/// </summary>
		/// <param name="fileName">File name, up to 15 characters</param>
		/// <param name="path">File path</param>
		public void AddFile(string fileName, string path) {
			lock (this.fileNameToPathDictionary) {
				if (string.IsNullOrWhiteSpace(fileName) || fileName.Length > 15) {
					throw new ArgumentException("name");
				}

				if (string.IsNullOrWhiteSpace(path)) {
					throw new ArgumentException("path");
				}

				if (CrossPlatformController.StorageController.FileExist(path) == false) {
					throw new ArgumentException("path", "File does not exist");
				}

				if (this.fileNameToPathDictionary.ContainsKey(fileName) == false) {
					this.fileNameToPathDictionary.Add(fileName, path);
				}
				else {
					this.fileNameToPathDictionary[fileName] = path;
				}
				this.AreAllFilesUpToDate = false;
			}
		}
		/// <summary>
		/// Removes a image from the collection
		/// </summary>
		/// <param name="fileName">Image name, up to 15 characters</param>
		public void RemoveFile(string fileName) {
			lock (this.fileNameToPathDictionary) {
				this.fileNameToPathDictionary.Remove(fileName);
			}
		}
		/// <summary>
		/// Gets the file path currently assigned to the specified file name
		/// </summary>
		/// <param name="fileName">File Name</param>
		/// <returns>File Path or null if not found</returns>
		public string GetFilePath(string fileName) {
			lock (this.fileNameToPathDictionary) {
				if (this.fileNameToPathDictionary.ContainsKey(fileName) == true) {
					return this.fileNameToPathDictionary[fileName];
				}
				else {
					return null;
				}
			}
		}
		/// <summary>
		/// Loads all the files at the collection into the PinPad
		/// </summary>
		/// <param name="force">If true will force load all the files</param>
		/// <returns>true if all files were successfully loaded</returns>
		public bool LoadFiles(bool force = false) {
			lock (this.fileNameToPathDictionary) {
				bool wereAllFilesSuccessfullyLoaded = true;

				int counter = 0;
				foreach (string fileName in this.FileNameCollection) {
					counter++;

					string filePath = this.fileNameToPathDictionary[fileName];
					bool successfullyLoaded;
					if (force == true) {
						successfullyLoaded = this.LoadFile(fileName, filePath);
					}
					else {
						successfullyLoaded = this.LoadFileIfNotExistOrSizeDiffers(fileName, filePath);
					}
					if (this.OnFileLoaded != null) {
						this.OnFileLoaded(this, new PinpadFileLoadedEventArgs(this.communication, fileName, filePath, successfullyLoaded, counter, this.fileNameToPathDictionary.Count));
					}

					wereAllFilesSuccessfullyLoaded &= successfullyLoaded;
				}
				this.AreAllFilesUpToDate = wereAllFilesSuccessfullyLoaded;
				return wereAllFilesSuccessfullyLoaded;
			}
		}
		/// <summary>
		/// Checks if a file exists in the PinPad
		/// </summary>
		/// <param name="fileName">File name, up to 15 characters</param>
		/// <returns>true if exists, false if does not exist, null on error</returns>
		public Nullable<bool> FileExists(string fileName) {
			LfcRequest request = new LfcRequest( );
			request.LFC_FILENAME.Value = fileName;
			LfcResponse response = this.communication.SendRequestAndReceiveResponse<LfcResponse>(request);

			if (response == null || response.RSP_STAT.Value != AbecsResponseStatus.ST_OK) {
				return null;
			}
			else {
				return response.LFC_EXISTS.Value;
			}
		}
		/// <summary>
		/// Checks if a file exists in the PinPad and size is equal to the specified file path
		/// </summary>
		/// <param name="fileName">File name, up to 15 characters</param>
		/// <param name="path">File Path</param>
		/// <returns>true if exists and is equal, false if does not exist or is not equal, null on error</returns>
		public Nullable<bool> FileExistsAndHasSameSize(string fileName, string path) {
			LfcRequest request = new LfcRequest( );
			request.LFC_FILENAME.Value = fileName;
			LfcResponse response = this.communication.SendRequestAndReceiveResponse<LfcResponse>(request);

			if (response == null || response.RSP_STAT.Value != AbecsResponseStatus.ST_OK) {
				return null;
			}
			else {
				return CrossPlatformController.StorageController.OpenFile(path).Length == response.LFC_FILESIZE.Value;
			}
		}
		/// <summary>
		/// Loads a file if it does not exist or it's size differs
		/// </summary>
		/// <param name="fileName">File name, up to 15 characters</param>
		/// <param name="path">File path</param>
		/// <returns>false on error</returns>
		private bool LoadFileIfNotExistOrSizeDiffers(string fileName, string path) {
			Nullable<bool> fileExists = FileExistsAndHasSameSize(fileName, path);
			if (fileExists == null || fileExists == false) {
				return LoadFile(fileName, path);
			}
			else {
				return true;
			}
		}
		/// <summary>
		/// Loads a file to the PinPad
		/// </summary>
		/// <param name="fileName">File name, up to 15 characters</param>
		/// <param name="path">File Path</param>
		/// <returns>true if file was successfully loaded</returns>
		private bool LoadFile(string fileName, string path) {
			return LoadFile(CrossPlatformController.StorageController.OpenFile(path), fileName);
		}
		/// <summary>
		/// Loads a file to the PinPad
		/// </summary>
		/// <param name="stream">File Stream</param>
		/// <param name="fileName">File name, up to 15 characters</param>
		/// <returns>true if file was successfully loaded</returns>
		private bool LoadFile(Stream stream, string fileName) {
			if (this.StartLoadingFile(fileName) == false) {
				return false;
			}

			while (stream.Position < stream.Length) {
				byte[] buffer = new byte[256];
				int count = stream.Read(buffer, 0, 256);

				if (this.LoadFileData(buffer.Take(count).ToArray( )) == false) {
					return false;
				}
			}

			return this.FinishLoadingFile( );
		}
		private bool StartLoadingFile(string fileName) {
			LfiRequest request = new LfiRequest( );
			request.LFI_FILENAME.Value = fileName;

			return this.communication.SendRequestAndVerifyResponseCode(request);
		}
		private bool LoadFileData(byte[] data) {
			LfrRequest request = new LfrRequest( );
			request.LFR_DATA.Value = new HexadecimalData(data);

			return this.communication.SendRequestAndVerifyResponseCode(request);
		}
		private bool FinishLoadingFile( ) {
			LfeRequest request = new LfeRequest( );

			return this.communication.SendRequestAndVerifyResponseCode(request);
		}
	}
}