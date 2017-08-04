using System;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace Pinpad.Sdk.Test.Mockers
{
    public class ApplicationFileMocker
    {
		public static string MockNewApplicationFile(string fileName)
		{
            string path = AppDomain.CurrentDomain.BaseDirectory;

			// Creates mocked
			Random r = new Random();
			byte[] data = new byte[1800];

			r.NextBytes(data);

			// Creates file:
			string tempPath = ApplicationFileMocker.SaveData(path, data);

			// Zip the file:
			try
			{
				ZipFile.CreateFromDirectory(tempPath, Path.Combine(path, fileName), CompressionLevel.Fastest,
					true);
			}
			catch (IOException)
			{
				File.Delete(Path.Combine(path, fileName));
				Debug.WriteLine("O arquivo já existe na pasta de output.");
			}

			return Path.Combine(path, fileName);
		}
		public static void Unmock()
		{
<<<<<<< HEAD
			foreach (string fileName in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.zip"))
=======
			foreach (string fileName in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.zip"))
>>>>>>> develop
			{
				File.Delete(fileName);
			}

<<<<<<< HEAD
			Directory.Delete(Path.Combine(Directory.GetCurrentDirectory(), "temp"), true);
=======
			Directory.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp"), true);
>>>>>>> develop
		}

		private static string SaveData(string path, byte[] Data)
		{
			BinaryWriter writer = null;
			string name = Path.Combine(path, "temp", "temp.dat");

			if (Directory.Exists(Path.Combine(path, "temp")) == false)
			{
				Directory.CreateDirectory(Path.Combine(path, "temp"));
			}

			try
			{
				// Create a new stream to write to the file
				writer = new BinaryWriter(File.OpenWrite(name));

				// Writer raw data                
				writer.Write(Data);
				writer.Flush();
				writer.Close();
			}
			catch
			{
				//...
				return null;
			}

			return Path.Combine(path, "temp");
		}
    }
}
