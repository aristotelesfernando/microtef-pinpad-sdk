using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pinpad.Sdk.Model.TypeCode;
using LegacyResponseStatus = PinPadSDK.Enums.ResponseStatus;

namespace Pinpad.Sdk.Mapper
{
    internal class ResponseStatusMapper
    {
        internal static ResponseStatus MapLegacyResponseStatus(LegacyResponseStatus status)
        {
            switch (status)
            {
                case LegacyResponseStatus.ST_OK: return ResponseStatus.Ok;
                case LegacyResponseStatus.ST_NOSEC: return ResponseStatus.NotSecureCommunication;
                case LegacyResponseStatus.ST_F1: return ResponseStatus.F1KeyPressed;
                case LegacyResponseStatus.ST_F2: return ResponseStatus.F2KeyPressed;
                case LegacyResponseStatus.ST_F3: return ResponseStatus.F3KeyPressed;
                case LegacyResponseStatus.ST_F4: return ResponseStatus.F4KeyPressed;
                case LegacyResponseStatus.ST_BACKSP: return ResponseStatus.BackspacePressed;
                case LegacyResponseStatus.ST_ERRPKTSEC: return ResponseStatus.CancelKeyPressed;
                case LegacyResponseStatus.ST_INVCALL: return ResponseStatus.InvalidCommand;
                case LegacyResponseStatus.ST_INVPARM: return ResponseStatus.InvalidParameter;
                case LegacyResponseStatus.ST_TIMEOUT: return ResponseStatus.TimeOut;
                case LegacyResponseStatus.ST_CANCEL: return ResponseStatus.OperationCancelled;
                case LegacyResponseStatus.ST_NOTOPEN: return ResponseStatus.PinpadIsClosed;
                case LegacyResponseStatus.ST_MANDAT: return ResponseStatus.MandatoryParameterNotReceived;
                case LegacyResponseStatus.ST_TABVERDIF: return ResponseStatus.InvalidEmvTable;
                case LegacyResponseStatus.ST_TABERR: return ResponseStatus.CouldNotWriteTable;
                case LegacyResponseStatus.ST_INTERR: return ResponseStatus.InternalError;
                case LegacyResponseStatus.ST_MCDATAERR: return ResponseStatus.MagneticStripeError;
                case LegacyResponseStatus.ST_ERRKEY: return ResponseStatus.PinIndexNotFound;
                case LegacyResponseStatus.ST_NOCARD: return ResponseStatus.NoneCardDetected;
                case LegacyResponseStatus.ST_PINBUSY: return ResponseStatus.PinBusy;
                case LegacyResponseStatus.ST_RSPOVRFL: return ResponseStatus.ResponseDataOverflow;
                case LegacyResponseStatus.ST_NOSAM: return ResponseStatus.SamError;
                case LegacyResponseStatus.ST_DUMBCARD: return ResponseStatus.DumbCard;
                case LegacyResponseStatus.ST_ERRCARD: return ResponseStatus.CardCommunicationError;
                case LegacyResponseStatus.ST_CARDINVALIDAT: return ResponseStatus.InvalidCard;
                case LegacyResponseStatus.ST_CARDPROBLEMS: return ResponseStatus.CardProblems;
                case LegacyResponseStatus.ST_CARDINVDATA: return ResponseStatus.InconsistentCard;
                case LegacyResponseStatus.ST_CARDAPPNAV: return ResponseStatus.InvalidEmvApplication;
                case LegacyResponseStatus.ST_CARDAPPNAUT: return ResponseStatus.UnableToProcessEmvApplication;
                case LegacyResponseStatus.ST_ERRFALLBACK: return ResponseStatus.Fallback;
                case LegacyResponseStatus.ST_INVAMOUNT: return ResponseStatus.InvalidAmount;
                case LegacyResponseStatus.ST_CTLSSMULTIPLE: return ResponseStatus.MultipleContactlessDetected;
                case LegacyResponseStatus.ST_CTLSSCOMMERR: return ResponseStatus.CtlsCommunicationError;
                case LegacyResponseStatus.ST_CTLSSINVALIDAT: return ResponseStatus.InvalidCtlsCard;
                case LegacyResponseStatus.ST_CTLSSPROBLEMS: return ResponseStatus.CtlsCardProblems;
                case LegacyResponseStatus.ST_CTLSSAPPNAV: return ResponseStatus.InvalidCtlsApplication;
                case LegacyResponseStatus.ST_CTLSSAPPNAUT: return ResponseStatus.UnableToProcessCtlsApplication;
                case LegacyResponseStatus.ST_MFNFOUND: return ResponseStatus.FileNotFound;
                case LegacyResponseStatus.ST_MFERRFMT: return ResponseStatus.FileFormatError;
                case LegacyResponseStatus.ST_MFERR: return ResponseStatus.FileLoadingError;

                default: return ResponseStatus.Undefined;
            }
        }
    }
}
