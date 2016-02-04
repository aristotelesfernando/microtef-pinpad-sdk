using CrossPlatformBase;
using PinPadSDK.PinPad;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TransactionRouter.Controllers;
using TransactionRouter.Events;

namespace PinPadSdkTests {
    public class PinPadConnectionMock : IPinPadConnection {
        public event EventHandler OnOpen;
        public event EventHandler OnClose;
        public event EventHandler OnDispose;
        public event EventHandler OnDiscardInBuffer;
        public event EventHandler OnDiscardOutBuffer;
        public event EventHandler OnAcknowledgeFailed;
        public Func<int> GetWriteTimeout;
        public Action<int> SetWriteTimeout;
        public Func<int> GetReadTimeout;
        public Action<int> SetReadTimeout;
        public Func<string> GetConnectionName;
        public Func<bool> IsOpenFunc;
        public PinPadConnectionInputController ConnectionInput;
        public Queue<byte> ReadBuffer;
        public Action<string, bool> OnCommandReceived;
        public Action OnCommandCancelled;
        public bool ReplyToStoneKeepAlive = true;
        public bool InterceptCancelRequest = false;
        private int ReceivingChecksum = 0;

        public PinPadConnectionMock() {
            this.ConnectionInput = new PinPadConnectionInputController();
            this.ConnectionInput.OnCommandReceived += ConnectionInput_OnCommandReceived;
            this.ConnectionInput.OnCommandCancelled += ConnectionInput_OnCommandCancelled;

            this.ReadBuffer = new Queue<byte>();
        }
        void ConnectionInput_OnCommandReceived(object sender, CommandReceivedEventArgs e) {
            this.OnCommandReceived(e.commandRequest, e.checksumFailed);
        }
        public void SendNegativeAcknowledge() {
            lock (this.ReadBuffer) {
                this.ReadBuffer.Enqueue(0x15); //Send NAK
            }
        }
        public void SendPositiveAcknowledge() {
            lock (this.ReadBuffer) {
                this.ReadBuffer.Enqueue(0x06); //Send ACK
            }
        }
        public void WriteResponse(string response) {
            List<byte> responseByteCollection = new List<byte>(Encoding.ASCII.GetBytes(response));
            responseByteCollection.Add(0x17); //Add ETB

            this.WriteResponse(response, PinPadCommunication.GenerateChecksum(responseByteCollection.ToArray()));
        }
        public void WriteResponse(string response, byte[] checksum) {
            List<byte> responseByteCollection = new List<byte>(Encoding.ASCII.GetBytes(response));
            responseByteCollection.Add(0x17); //Add ETB
            responseByteCollection.AddRange(checksum); //Generate checksum
            responseByteCollection.Insert(0, 0x16); //Add SYN

            lock (this.ReadBuffer) {
                foreach (byte b in responseByteCollection) {
                    this.ReadBuffer.Enqueue(b);
                }
            }

        }
        void ConnectionInput_OnCommandCancelled(object sender, EventArgs e) {
            if (this.OnCommandCancelled != null) {
                this.OnCommandCancelled();
            }

            lock (this.ReadBuffer) {
                this.ReadBuffer.Enqueue(0x04); //Send EOT
            }
        }
        public void Open() {
            if (this.OnOpen != null) {
                this.OnOpen(this, EventArgs.Empty);
            }
        }
        public void Close() {
            if (this.OnClose != null) {
                this.OnClose(this, EventArgs.Empty);
            }
        }
        public void Dispose() {
            if (this.OnDispose != null) {
                this.OnDispose(this, EventArgs.Empty);
            }
        }
        public void DiscardInBuffer() {
            if (this.OnDiscardInBuffer != null) {
                this.OnDiscardInBuffer(this, EventArgs.Empty);
            }
        }
        public void DiscardOutBuffer() {
            if (this.OnDiscardOutBuffer != null) {
                this.OnDiscardOutBuffer(this, EventArgs.Empty);
            }
        }
        public void Write(byte[] buffer) {
            foreach (byte b in buffer) {
                this.WriteByte(b);
            }
        }
        public void WriteByte(byte data) {
            if (this.ReceivingChecksum == 0 && this.ReplyToStoneKeepAlive == true && data == 0x00) {
                lock (this.ReadBuffer) {
                    this.ReadBuffer.Enqueue(0x00); //Reply Stone Ping
                }
            }
            else if (this.ReceivingChecksum == 0 && this.InterceptCancelRequest == true && data == 0x18) {
                return;
            }
            else if (this.ReceivingChecksum == 0 && data == 0x15) {
                this.OnAcknowledgeFailed(this, EventArgs.Empty);
            }
            else {
                this.ConnectionInput.ReadByte(data);
            }

            if (data == 0x17) {
                this.ReceivingChecksum = 2;
            }
            else if (this.ReceivingChecksum > 0) {
                this.ReceivingChecksum--;
            }
        }
        public byte ReadByte() {
            Stopwatch timeout = Stopwatch.StartNew();
            while (this.ReadBuffer.Count == 0) {
                if (ReadTimeout != -1 && timeout.ElapsedMilliseconds > ReadTimeout) {
                    throw new TimeoutException();
                }
                Thread.Sleep(1);
            }

            lock (this.ReadBuffer) {
                return this.ReadBuffer.Dequeue();
            }
        }
        private int writeTimeout = 2000;
        public int WriteTimeout {
            get {
                if (this.GetWriteTimeout != null) {
                    return this.GetWriteTimeout();
                }
                else {
                    return this.writeTimeout;
                }
            }
            set {
                if (this.SetWriteTimeout != null) {
                    this.SetWriteTimeout(value);
                }
                else {
                    this.writeTimeout = value;
                }
            }
        }
        private int readTimeout = 2000;
        public int ReadTimeout {
            get {
                if (this.GetReadTimeout != null) {
                    return this.GetReadTimeout();
                }
                else {
                    return this.readTimeout;
                }
            }
            set {
                if (this.SetReadTimeout != null) {
                    this.SetReadTimeout(value);
                }
                else {
                    this.readTimeout = value;
                }
            }
        }
        public string ConnectionName {
            get {
                if (this.GetConnectionName != null) {
                    return this.GetConnectionName();
                }
                else {
                    return "COM1";
                }
            }
        }
        public bool IsOpen {
            get {
                if (this.IsOpenFunc != null) {
                    return this.IsOpenFunc();
                }
                else {
                    return true;
                }

            }
        }
        public int BytesToRead {
            get { return this.ReadBuffer.Count; }
        }
    }
}
