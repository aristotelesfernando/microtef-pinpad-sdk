using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace Pinpad.Sdk.Test.Mockings
{
    public abstract class ApplicationFileMocker
    {
        public static string MockNewApplicationFile(string fileName)
        {
            string path = Directory.GetCurrentDirectory();

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
            catch (IOException ex)
            {
                File.Delete(Path.Combine(path, fileName));
                Debug.WriteLine("O arquivo já existe na pasta de output.");
            }

            return Path.Combine(path, fileName);
        }
        public static void Unmock ()
        {
            foreach (string fileName in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.zip"))
            {
                File.Delete(fileName);
            }

            Directory.Delete(Path.Combine(Directory.GetCurrentDirectory(), "temp"), true);
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
