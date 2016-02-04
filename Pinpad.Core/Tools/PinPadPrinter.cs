using PinPadSDK.Commands;
using PinPadSDK.Controllers;
using PinPadSDK.Exceptions;
using PinPadSDK.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Tools
{
    public enum PinPadPrinterSize
    {
        Small = 0,
        //Size1,
        Medium = 2,
        //Size3,
        //Size4,
        //Size5,
        Big = 6,
        //Size7,
    }

    public enum PinPadPrinterAlignment
    {
        Left = 0,
        Center,
        Right
    }

    public class PinPadPrinter
    {
        public PinPadConnectionController WriteTool { get; private set; }

        public PinPadPrinter(PinPadConnectionController WriteTool)
        {
            this.WriteTool = WriteTool;
        }

        public bool IsPrinterSupported()
        {
            if (WriteTool.GetStoneVersion() == 0)
                return false;

            GINRequest Request = new GINRequest();
            Request.GIN_ACQIDX = 0;

            GINResponse Response = WriteTool.WriteRequest < GINResponse>(Request);

            if (Response == null || Response.RSP_STAT != ReturnCodes.ST_OK)
                return false;

            switch (Response.GIN_MODEL.Trim())
            {
                case "D210":
                    return true;
                default:
                    return false;
            }
        }

        public void Begin()
        {
            if (WriteTool.GetStoneVersion() == 0) {
                throw new NoStoneAppException();
            }

            PRTRequest Request = new PRTRequest();
            Request.PRT_Action = (int)PRTAction.Begin;

            PRTResponse Response = SendPRT(Request);

            if (Response == null){
                throw new PinPadTimeoutException(Request, Response);
            }

            if (Response.RSP_STAT != ReturnCodes.ST_OK) {
                throw new PinPadResponseStatusException(Request, Response);
            }

            if (Response.PRT_STATUS != 0){
                throw PrinterExceptionFactory.Create(Request, Response);
            }
        }

        public bool End()
        {
            if (WriteTool.GetStoneVersion() == 0)
                return false;

            PRTRequest Request = new PRTRequest();
            Request.PRT_Action = (int)PRTAction.End;

            PRTResponse Response = SendPRT(Request);

            if (Response == null || Response.RSP_STAT != ReturnCodes.ST_OK)
                return false;
            else
                return true;
        }

        public bool Print(string Message, PinPadPrinterSize Size, PinPadPrinterAlignment Alignment)
        {
            if (WriteTool.GetStoneVersion() == 0)
                return false;

            PRTRequest Request = new PRTRequest();
            Request.PRT_Action = (int)PRTAction.Print;
            Request.PRT_Data = Message;
            Request.PRT_Size = (int)Size;
            Request.PRT_Alignment = (int)Alignment;

            PRTResponse Response = SendPRT(Request);

            if (Response == null || Response.RSP_STAT != ReturnCodes.ST_OK)
                return false;
            else
                return true;
        }

        public bool PrintImage(string FileName, int Horizontal)
        {
            if (WriteTool.GetStoneVersion() == 0)
                return false;

            PRTRequest Request = new PRTRequest();
            Request.PRT_Action = (int)PRTAction.PrintImage;
            Request.PRT_Data = FileName;
            Request.PRT_Horizontal = Horizontal;

            PRTResponse Response = SendPRT(Request);

            if (Response == null || Response.RSP_STAT != ReturnCodes.ST_OK)
                return false;
            else
                return true;
        }

        public bool Step(int Steps)
        {
            if (WriteTool.GetStoneVersion() == 0)
                return false;

            PRTRequest Request = new PRTRequest();
            Request.PRT_Action = (int)PRTAction.Step;
            Request.PRT_Steps = Steps;

            PRTResponse Response = SendPRT(Request);

            if (Response == null || Response.RSP_STAT != ReturnCodes.ST_OK)
                return false;
            else
                return true;
        }

        private PRTResponse SendPRT(PRTRequest Request)
        {
            int OriginalTimeout = WriteTool.Timeout;
            WriteTool.Timeout = 0;

            PRTResponse Response = WriteTool.WriteRequest < PRTResponse>(Request);

            WriteTool.Timeout = OriginalTimeout;
            return Response;
        }
    }
}
