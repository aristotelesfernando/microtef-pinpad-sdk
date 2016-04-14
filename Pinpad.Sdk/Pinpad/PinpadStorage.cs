using System.Linq;
using System.Collections.Generic;
using System;
using Pinpad.Sdk.TypeCode;
using System.IO;
using Pinpad.Sdk.Events;
using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Utilities;
using MicroPos.CrossPlatform;

namespace Pinpad.Sdk.Pinpad
{
	/// <summary>
	/// PinPad storage adapter
	/// </summary>
	public class PinpadStorage
	{
		// Constants
		internal const short DEFAULT_BUFFER_SIZE = 256;

		// Members
		/// <summary>
		/// Pinpad communication provider.
		/// </summary>
		private PinpadCommunication communication { get; set; }
		/// <summary>
		/// Dictionary containing the name file related to it's path.
		/// </summary>
		private Dictionary<string, string> files { get; set; }
		/// <summary>
		/// Gets a new array containing the all file names used.
		/// </summary>
		public string [] FileNameCollection
		{
			get
			{
				lock (this.files)
				{
					return this.files.Keys.ToArray();
				}
			}
		}
		/// <summary>
		/// Are all the files loaded into the Pinpad?
		/// </summary>
		public bool AreAllFilesUpToDate { get; private set; }
		/// <summary>
		/// Event called when a file is fully loaded into the Pinpad.
		/// </summary>
		public event EventHandler<PinpadFileLoadedEventArgs> OnFileLoaded;

		// Constructor
		/// <summary>
		/// Basic constructor.
		/// </summary>
		/// <param name="commuication">Pinpad communication provider.</param>
		public PinpadStorage(PinpadCommunication communication)
		{
			this.communication = communication;
			this.files = new Dictionary<string, string>();
			this.AreAllFilesUpToDate = true;
		}

		// Methods
		/// <summary>
		/// Adds a file to the collection.
		/// </summary>
		/// <param name="fileName">File name, up to 15 characters.</param>
		/// <param name="path">File path.</param>
		public void AddFile(string fileName, string path)
		{
			lock (this.files)
			{
				// Validating parameters:
				if (string.IsNullOrWhiteSpace(fileName) || fileName.Length > 15)
				{
					throw new ArgumentException("name");
				}
				if (string.IsNullOrWhiteSpace(path))
				{
					throw new ArgumentException("path");
				}

				// Verifies if the file exists:
				if (CrossPlatformController.StorageController.FileExist(path) == false)
				{
					throw new ArgumentException("path", "File does not exist");
				}

				// Verifies if the files is already in the collection, that is, if the file
				// was already loaded:
				if (this.files.ContainsKey(fileName) == false)
				{
					// Add the file if it wasn't loaded yet:
					this.files.Add(fileName, path);
				}
				
				// The file exists, but the path has changed:
				else { this.files[fileName] = path; }

				// Indicates that something has change. In this case, that a file has been added:
				this.AreAllFilesUpToDate = false;
			}
		}
		/// <summary>
		/// Removes a file from the collection of files.
		/// </summary>
		/// <param name="fileName">File name, up to 15 characters.</param>
		public void RemoveFile(string fileName)
		{
			lock (this.files)
			{
				this.files.Remove(fileName);
			}
		}
		/// <summary>
		/// Gets the file path currently assigned to the specified file name.
		/// </summary>
		/// <param name="fileName">File name, up to 15 characters.</param>
		/// <returns>File path, or null if not found.</returns>
		public string GetFilePath(string fileName)
		{
			lock (this.files)
			{
				if (this.files.ContainsKey(fileName) == true)
				{
					return this.files[fileName];
				}

				else { return null; }
			}
		}
		/// <summary>
		/// Loads all the files at the collection into the PinPad
		/// </summary>
		/// <param name="force">If true will force load all the files</param>
		/// <returns>true if all files were successfully loaded</returns>
		public bool LoadFiles(bool force = false)
		{
			lock (this.files)
			{
				bool wereAllFilesSuccessfullyLoaded = true;

				int counter = 0;
				foreach (string fileName in this.FileNameCollection)
				{
					counter++;

					string filePath = this.files[fileName];
					bool successfullyLoaded;
					if (force == true)
					{
						successfullyLoaded = this.LoadFile(fileName, filePath);
					}
					else
					{
						successfullyLoaded = this.LoadFileIfNotExistOrSizeDiffers(fileName, filePath);
					}
					if (this.OnFileLoaded != null)
					{
						this.OnFileLoaded(this, new PinpadFileLoadedEventArgs(this.communication, fileName, filePath, successfullyLoaded, counter, this.files.Count));
					}

					wereAllFilesSuccessfullyLoaded &= successfullyLoaded;
				}

				this.AreAllFilesUpToDate = wereAllFilesSuccessfullyLoaded;

				return wereAllFilesSuccessfullyLoaded;
			}
		}
		/// <summary>
		/// Checks if a file exists in the Pinpad.
		/// </summary>
		/// <param name="fileName">File name, up to 15 characters.</param>
		/// <returns>True the file exists, false if does not exist, null on error.</returns>
		public Nullable<bool> FileExists(string fileName)
		{
			LfcRequest request = new LfcRequest();
			request.LFC_FILENAME.Value = fileName;
			LfcResponse response = this.communication.SendRequestAndReceiveResponse<LfcResponse>(request);

			if (response == null || response.RSP_STAT.Value != AbecsResponseStatus.ST_OK)
			{
				return null;
			}
			else
			{
				return response.LFC_EXISTS.Value;
			}
		}
		/// <summary>
		/// Checks if a file exists in the pinpad and it's size is equal to the specified file path.
		/// </summary>
		/// <param name="fileName">File name, up to 15 characters.</param>
		/// <param name="path">File path.</param>
		/// <returns>True if exists and is equal, false if does not exist or is not equal, null on error.</returns>
		public Nullable<bool> FileExistsAndHasSameSize(string fileName, string path)
		{
			LfcRequest request = new LfcRequest();
			request.LFC_FILENAME.Value = fileName;
			LfcResponse response = this.communication.SendRequestAndReceiveResponse<LfcResponse>(request);

			if (response == null || response.RSP_STAT.Value != AbecsResponseStatus.ST_OK)
			{
				return null;
			}
			else
			{
				return CrossPlatformController.StorageController.OpenFile(path).Length == response.LFC_FILESIZE.Value;
			}
		}
		/// <summary>
		/// Loads a file if it does not exist or it's size differs.
		/// </summary>
		/// <param name="fileName">File name, up to 15 characters.</param>
		/// <param name="path">File path.</param>
		/// <returns>False on error.</returns>
		private bool LoadFileIfNotExistOrSizeDiffers(string fileName, string path)
		{
			Nullable<bool> fileExists = FileExistsAndHasSameSize(fileName, path);
			if (fileExists == null || fileExists == false)
			{
				return LoadFile(fileName, path);
			}

			else { return true; }
		}
		/// <summary>
		/// Loads a file into the pinpad.
		/// </summary>
		/// <param name="fileName">File name, up to 15 characters.</param>
		/// <param name="path">File path.</param>
		/// <returns>True if file was successfully loaded.</returns>
		private bool LoadFile(string fileName, string path)
		{
			return LoadFile(CrossPlatformController.StorageController.OpenFile(path), fileName);
		}
		/// <summary>
		/// Loads a file into the pinpad.
		/// </summary>
		/// <param name="stream">File stream.</param>
		/// <param name="fileName">File name, up to 15 characters.</param>
		/// <returns>True if file was successfully loaded.</returns>
		private bool LoadFile(Stream stream, string fileName)
		{
			if (this.StartLoadingFile(fileName) == false) { return false; }

			while (stream.Position < stream.Length)
			{
				byte[] buffer = new byte[DEFAULT_BUFFER_SIZE];

				// Reads 256 bytes from the stream:
				int count = stream.Read(buffer, 0, DEFAULT_BUFFER_SIZE);

				if (this.LoadFileData(buffer.Take(count).ToArray()) == false)
				{
					return false;
				}
			}
			
			// Sends a request to the pinpad, indicating that there's no more files to be loaded:
			return this.FinishLoadingFile();
		}
		/// <summary>
		/// Sends a command to the pinpad, indicating that a file loading is about to begin.
		/// </summary>
		/// <param name="fileName">File name, up to 15 characters.</param>
		/// <returns>If the operation succeed.</returns>
		private bool StartLoadingFile(string fileName)
		{
			LfiRequest request = new LfiRequest();
			request.LFI_FILENAME.Value = fileName;

			return this.communication.SendRequestAndVerifyResponseCode(request);
		}
		/// <summary>
		/// Loads a file into the pinpad.
		/// </summary>
		/// <param name="data">Data to be loaded.</param>
		/// <returns>If the operation succeed.</returns>
		private bool LoadFileData(byte [] data)
		{
			LfrRequest request = new LfrRequest();
			request.LFR_DATA.Value = new HexadecimalData(data);

			return this.communication.SendRequestAndVerifyResponseCode(request);
		}
		/// <summary>
		/// Sends a command to the pinpad, indicating that there's no more files to be loaded.
		/// </summary>
		/// <returns>If the operation succeed.</returns>
		private bool FinishLoadingFile()
		{
			LfeRequest request = new LfeRequest( );

			return this.communication.SendRequestAndVerifyResponseCode(request);
		}
	}
}