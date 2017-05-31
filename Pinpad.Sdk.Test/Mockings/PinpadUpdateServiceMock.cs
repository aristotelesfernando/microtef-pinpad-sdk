using System;
using Pinpad.Sdk.Model.Pinpad;
using System.Diagnostics;

namespace Pinpad.Sdk.Test.Mockings
{
    public sealed class PinpadUpdateServiceMock : IPinpadUpdateService
    {
        public Version CurrentApplicationVersion { get; private set; } = new Version("2.32.7");
        public int SectionSize
        {
            get { return this.RandomValueProvider.Next(1, 900); }
        }
        
        public bool Load(string filePath)
        {
            Debug.WriteLine("Loading file into memory.");
            return true;
        }
        public bool Update()
        {
            Debug.WriteLine("Updating pinpad app.");
            return true;
        }

        private Random RandomValueProvider = new Random();
    }
}
