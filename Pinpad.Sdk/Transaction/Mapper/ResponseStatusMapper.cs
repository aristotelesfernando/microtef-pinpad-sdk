using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Model;

namespace Pinpad.Sdk.Transaction
{
    internal class ResponseStatusMapper
    {
        internal static ResponseStatus MapLegacyResponseStatus(AbecsResponseStatus status)
		{
            switch (status)
            {
                case AbecsResponseStatus.ST_OK: return ResponseStatus.Ok;
                case AbecsResponseStatus.ST_NOSEC: return ResponseStatus.NotSecureCommunication;
                case AbecsResponseStatus.ST_F1: return ResponseStatus.F1KeyPressed;
                case AbecsResponseStatus.ST_F2: return ResponseStatus.F2KeyPressed;
                case AbecsResponseStatus.ST_F3: return ResponseStatus.F3KeyPressed;
                case AbecsResponseStatus.ST_F4: return ResponseStatus.F4KeyPressed;
                case AbecsResponseStatus.ST_BACKSP: return ResponseStatus.BackspacePressed;
                case AbecsResponseStatus.ST_ERRPKTSEC: return ResponseStatus.CancelKeyPressed;
                case AbecsResponseStatus.ST_INVCALL: return ResponseStatus.InvalidCommand;
                case AbecsResponseStatus.ST_INVPARM: return ResponseStatus.InvalidParameter;
                case AbecsResponseStatus.ST_TIMEOUT: return ResponseStatus.TimeOut;
                case AbecsResponseStatus.ST_CANCEL: return ResponseStatus.OperationCancelled;
                case AbecsResponseStatus.ST_NOTOPEN: return ResponseStatus.PinpadIsClosed;
                case AbecsResponseStatus.ST_MANDAT: return ResponseStatus.MandatoryParameterNotReceived;
                case AbecsResponseStatus.ST_TABVERDIF: return ResponseStatus.InvalidEmvTable;
                case AbecsResponseStatus.ST_TABERR: return ResponseStatus.CouldNotWriteTable;
                case AbecsResponseStatus.ST_INTERR: return ResponseStatus.InternalError;
                case AbecsResponseStatus.ST_MCDATAERR: return ResponseStatus.MagneticStripeError;
                case AbecsResponseStatus.ST_ERRKEY: return ResponseStatus.PinIndexNotFound;
                case AbecsResponseStatus.ST_NOCARD: return ResponseStatus.NoneCardDetected;
                case AbecsResponseStatus.ST_PINBUSY: return ResponseStatus.PinBusy;
                case AbecsResponseStatus.ST_RSPOVRFL: return ResponseStatus.ResponseDataOverflow;
                case AbecsResponseStatus.ST_NOSAM: return ResponseStatus.SamError;
                case AbecsResponseStatus.ST_DUMBCARD: return ResponseStatus.DumbCard;
                case AbecsResponseStatus.ST_ERRCARD: return ResponseStatus.CardCommunicationError;
                case AbecsResponseStatus.ST_CARDINVALIDAT: return ResponseStatus.InvalidCard;
                case AbecsResponseStatus.ST_CARDPROBLEMS: return ResponseStatus.CardProblems;
                case AbecsResponseStatus.ST_CARDINVDATA: return ResponseStatus.InconsistentCard;
                case AbecsResponseStatus.ST_CARDAPPNAV: return ResponseStatus.InvalidEmvApplication;
                case AbecsResponseStatus.ST_CARDAPPNAUT: return ResponseStatus.UnableToProcessEmvApplication;
                case AbecsResponseStatus.ST_ERRFALLBACK: return ResponseStatus.Fallback;
                case AbecsResponseStatus.ST_INVAMOUNT: return ResponseStatus.InvalidAmount;
                case AbecsResponseStatus.ST_CTLSSMULTIPLE: return ResponseStatus.MultipleContactlessDetected;
                case AbecsResponseStatus.ST_CTLSSCOMMERR: return ResponseStatus.CtlsCommunicationError;
                case AbecsResponseStatus.ST_CTLSSINVALIDAT: return ResponseStatus.InvalidCtlsCard;
                case AbecsResponseStatus.ST_CTLSSPROBLEMS: return ResponseStatus.CtlsCardProblems;
                case AbecsResponseStatus.ST_CTLSSAPPNAV: return ResponseStatus.InvalidCtlsApplication;
                case AbecsResponseStatus.ST_CTLSSAPPNAUT: return ResponseStatus.UnableToProcessCtlsApplication;
                case AbecsResponseStatus.ST_MFNFOUND: return ResponseStatus.FileNotFound;
                case AbecsResponseStatus.ST_MFERRFMT: return ResponseStatus.FileFormatError;
                case AbecsResponseStatus.ST_MFERR: return ResponseStatus.FileLoadingError;

                default: return (ResponseStatus) status;
            }
        }
    }
}
