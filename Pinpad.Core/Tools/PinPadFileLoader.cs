using CrossPlatformBase;
using Dlp.Buy4.Switch.Utils;
using PinPadSDK.Commands;
using PinPadSDK.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PinPadSDK.Tools
{
    public class PinPadFileLoader
    {
        public PinPadConnectionController WriteTool { get; private set; }

        public PinPadFileLoader(PinPadConnectionController WriteTool)
        {
            this.WriteTool = WriteTool;
        }

        public bool LoadFileIfNotExist(String Path, String FileName)
        {
            if (WriteTool.GetStoneVersion() == 0)
                return false;

            bool? Exists = FileExists(FileName);
            if (Exists == null || Exists == false)
            {
                return LoadFile(Path, FileName);
            }
            else
            {
                return true;
            }
        }

        public bool LoadFileIfNotExistOrNotEqual(String Path, String FileName)
        {
            if (WriteTool.GetStoneVersion() == 0)
                return false;

            bool? FileExists = FileExistsAndIsEqual(Path, FileName);
            if (FileExists == null || FileExists == false)
            {
                return LoadFile(Path, FileName);
            }
            else
            {
                return true;
            }
        }

        public bool? FileExists(String FileName)
        {
            if (WriteTool.GetStoneVersion() == 0)
                return null;

            LFCRequest Request = new LFCRequest();
            Request.LFC_FileName = FileName;
            LFCResponse Response = WriteTool.WriteRequest<LFCResponse>(Request);

            if (Response == null || Response.RSP_STAT != ReturnCodes.ST_OK)
                return null;

            return Response.LFC_EXISTS;
        }

        public bool? FileExistsAndIsEqual(String Path, String FileName)
        {
            if (WriteTool.GetStoneVersion() == 0)
                return null;

            LFCRequest Request = new LFCRequest();
            Request.LFC_FileName = FileName;
            LFCResponse Response = WriteTool.WriteRequest<LFCResponse>(Request);

            if (Response == null || Response.RSP_STAT != ReturnCodes.ST_OK)
                return null;

            if (!Response.LFC_FILESIZE.HasValue)
                return false;

            return CrossPlatformController.StorageController.OpenFile(Path).Length == Response.LFC_FILESIZE.Value;
        }

        public bool LoadFile(String Path, String FileName)
        {
            return LoadFile(CrossPlatformController.StorageController.OpenFile(Path), FileName);
        }

        public bool LoadFile(Stream stream, String FileName)
        {
            if (WriteTool.GetStoneVersion() == 0)
                return false;

            LFIRequest LFIrequest = new LFIRequest();
            LFIrequest.LFI_FileName = FileName;
            LFIResponse LFIresponse = WriteTool.WriteRequest<LFIResponse>(LFIrequest);
            if (LFIresponse == null || LFIresponse.RSP_STAT != ReturnCodes.ST_OK)
                return false;

            while (stream.Position < stream.Length)
            {
                byte[] Buffer = new byte[256];
                int Length = stream.Read(Buffer, 0, 256);
                string data = HexUtils.FromBytes(Buffer, Length);

                LFRRequest LFRrequest = new LFRRequest();
                LFRrequest.LFR_Data = data;
                LFRResponse LFRresponse = WriteTool.WriteRequest<LFRResponse>(LFRrequest);
                if (LFRresponse == null || LFRresponse.RSP_STAT != ReturnCodes.ST_OK)
                    return false;
            }

            LFERequest LFErequest = new LFERequest();
            LFEResponse LFEresponse = WriteTool.WriteRequest<LFEResponse>(LFErequest);
            if (LFEresponse == null || LFEresponse.RSP_STAT != ReturnCodes.ST_OK)
                return false;
            else
                return true;
        }
    }
}
