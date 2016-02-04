using CrossPlatformBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinPadSdkTests {
    public class SystemStorageMock : IStorageController {
        private Dictionary<string, byte[]> fileDataDictionary = new Dictionary<string, byte[]>();

        public void AddFile(string path, byte[] data) {
            lock (this.fileDataDictionary) {
                if (this.fileDataDictionary.ContainsKey(path) == true) {
                    this.fileDataDictionary[path] = data;
                }
                else {
                    this.fileDataDictionary.Add(path, data);
                }
            }
        }

        public Stream CreateFile(string path, bool append = true) {
            throw new NotImplementedException();
        }

        public Stream OpenFile(string path) {
            lock (this.fileDataDictionary) {
                if (this.FileExist(path) == false) {
                    throw new IOException();
                }
                MemoryStream fileStream = new MemoryStream(this.fileDataDictionary[path]);
                return fileStream;
            }
        }

        public bool FileExist(string path) {
            lock (this.fileDataDictionary) {
                return this.fileDataDictionary.ContainsKey(path);
            }
        }
    }
}
