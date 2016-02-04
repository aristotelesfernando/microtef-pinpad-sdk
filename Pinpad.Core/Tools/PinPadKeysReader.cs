using PinPadSDK.Commands;
using PinPadSDK.Controllers;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Tools
{
    public class PinPadKeysReader
    {
        public static PinPadKeyEnum ReadKey(PinPadConnectionController WriteTool)
        {
            if (WriteTool.GetStoneVersion() > 0)
            {
                ClearKeyExtendedBuffer(WriteTool);
                PinPadKeyEnum Key = ReadKeyExtended(WriteTool);
                ClearKeyExtendedBuffer(WriteTool);
                return Key;
            }
        Retry:
            GKYRequest Request = new GKYRequest();
            int OriginalTimeout = WriteTool.Timeout;
            WriteTool.Timeout = 0;
            GKYResponse Response = WriteTool.WriteRequest < GKYResponse>(Request);
            WriteTool.Timeout = OriginalTimeout;
            if (Response == null)
            {
                if (WriteTool.Abort)
                    return PinPadKeyEnum.CANCEL;
                goto Retry;
            }

            return Response.GKY_KEYCODE;
        }

        public static PinPadKeyEnum ReadKeyExtended(PinPadConnectionController WriteTool)
        {
            if (WriteTool.GetStoneVersion() <= 0)
                return PinPadKeyEnum.CANCEL;

            Retry:
            GKERequest Request = new GKERequest();
            Request.GKE_ACTION = (int)GKEActions.GKE_ReadKey - 1;

            int OriginalTimeout = WriteTool.Timeout;
            WriteTool.Timeout = 0;
            GKEResponse Response = WriteTool.WriteRequest < GKEResponse>(Request);
            WriteTool.Timeout = OriginalTimeout;
            if (Response == null)
            {
                if (WriteTool.Abort)
                    return PinPadKeyEnum.CANCEL;
                goto Retry;
            }

            return Response.GKE_KEYCODE;
        }

        public static bool ClearKeyExtendedBuffer(PinPadConnectionController WriteTool)
        {
            if (WriteTool.GetStoneVersion() <= 0)
                return false;

        Retry:
            GKERequest Request = new GKERequest();
            Request.GKE_ACTION = (int)GKEActions.GKE_ClearBuffer - 1;

            int OriginalTimeout = WriteTool.Timeout;
            WriteTool.Timeout = 0;
            GKEResponse Response = WriteTool.WriteRequest<GKEResponse>(Request);
            WriteTool.Timeout = OriginalTimeout;
            if (Response == null)
            {
                if (WriteTool.Abort)
                    return false;
                goto Retry;
            }

            if (Response.RSP_STAT == ReturnCodes.ST_OK)
                return true;
            else
                return false;
        }
    }
}
