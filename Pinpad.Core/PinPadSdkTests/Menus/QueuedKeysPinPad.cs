using PinPadSDK.Commands;
using PinPadSDK.Commands.Stone;
using PinPadSDK.Enums;
using PinPadSDK.PinPad;
using PinPadSDK.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinPadSdkTests.Menus {
    public class QueuedKeysPinPad {
        public Queue<PinPadKey> KeyCollection { get; set; }
        public PinPadConnectionMock PinPadConnection { get; set; }

        public Action<string, bool> OnCommandReceived;

        public Action<SimpleMessage> OnSimpleMessageReceived;
        public Action<MultilineMessage> OnMultilineMessageReceived;

        public bool SupportStone { get; set; }
        public QueuedKeysPinPad() {
            this.KeyCollection = new Queue<PinPadKey>();
            this.PinPadConnection = new PinPadConnectionMock();
            this.SupportStone = false;
            this.PinPadConnection.OnCommandReceived = PinPadConnection_OnCommandReceived;
        }
        public QueuedKeysPinPad(params PinPadKey[] keys)
            : this() {
            foreach (PinPadKey key in keys) {
                this.KeyCollection.Enqueue(key);
            }
        }
        public void PinPadConnection_OnCommandReceived(string command, bool checksumFailed) {
            if (command.StartsWith("SEC") == true) {
                SecRequest secureRequest = new SecRequest();
                secureRequest.CommandString = command;

                SecResponse secureResponse = new SecResponse();
                secureResponse.SEC_CMDBLK.Value = secureRequest.SEC_CMDBLK.Value;

                command = PinPadEncryption.Instance.UnwrapResponse(secureResponse);
            }

            if (command == "OPN") {
                this.PinPadConnection.SendPositiveAcknowledge();
                if (this.SupportStone == true) {
                    this.PinPadConnection.WriteResponse("OPN000003001");
                }
                else {
                    this.PinPadConnection.WriteResponse("OPN000");
                }
            }
            else if (command.StartsWith("DSP")) {
                this.PinPadConnection.SendPositiveAcknowledge();
                this.PinPadConnection.WriteResponse("DSP000");
                if (this.OnSimpleMessageReceived != null) {
                    DspRequest request = new DspRequest();
                    request.CommandString = command;
                    this.OnSimpleMessageReceived(request.DSP_MSG.Value);
                }
            }
            else if (command.StartsWith("DEX")) {
                this.PinPadConnection.SendPositiveAcknowledge();
                this.PinPadConnection.WriteResponse("DEX000");
                if (this.OnMultilineMessageReceived != null) {
                    DexRequest request = new DexRequest();
                    request.CommandString = command;
                    this.OnMultilineMessageReceived(request.DEX_MSG.Value);
                }
            }
            else if (command == "GKY") {
                PinPadKey key = this.TakeNextKey();
                if (key != PinPadKey.Undefined) {
                    this.PinPadConnection.SendPositiveAcknowledge();

                    GkyResponse response = new GkyResponse();
                    response.PressedKey = key;
                    this.PinPadConnection.WriteResponse(response.CommandString);
                }
            }
            else if (this.SupportStone == true && command.StartsWith("GKE") == true) {
                GkeRequest request = new GkeRequest();
                request.CommandString = command;

                if (request.GKE_ACTION.Value == GkeAction.GKE_ClearBuffer) {
                    this.PinPadConnection.SendPositiveAcknowledge();
                    GkeResponse response = new GkeResponse();
                    response.RSP_STAT.Value = ResponseStatus.ST_OK;
                    this.PinPadConnection.WriteResponse(response.CommandString);
                }
                else {
                    PinPadKey key = this.TakeNextKey();
                    if (key != PinPadKey.Undefined) {
                        this.PinPadConnection.SendPositiveAcknowledge();

                        GkeResponse response = new GkeResponse();
                        response.PressedKey = key;
                        this.PinPadConnection.WriteResponse(response.CommandString);
                    }
                }
            }
            if (this.OnCommandReceived != null) {
                this.OnCommandReceived(command, checksumFailed);
            }
        }
        private PinPadKey TakeNextKey() {
            if (this.KeyCollection.Count == 0) {
                return PinPadKey.Undefined;
            }
            return this.KeyCollection.Dequeue();
        } 
    }
}
