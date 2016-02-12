using PinPadSDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinPadSDK.Commands.Stone;
using PinPadSDK.Enums;
using PinPadSDK.Commands.Stone.PrtActionData;

namespace PinPadSDK.Factories {
    /// <summary>
    /// Factory methods for printer exceptions
    /// </summary>
    public class PrinterExceptionFactory {
        /// <summary>
        /// Creates a Printer Exception based on a Printer Status
        /// </summary>
        /// <param name="status">Printer Status</param>
        /// <returns>PrinterException</returns>
        public static PrinterException Create(PinPadPrinterStatus status) {
            switch (status) {
                case PinPadPrinterStatus.Undefined: throw new InvalidOperationException("Attempt to create a PrinterException without a Printer Status");
                case PinPadPrinterStatus.Ready: return null; //Sucesso
                case PinPadPrinterStatus.Busy: return new PrinterBusyException();
                case PinPadPrinterStatus.OutOfPaper: return new PrinterOutOfPaperException();
                case PinPadPrinterStatus.InvalidFormat: return new PrinterException(status, "Formato da informação para imprimir inválido");
                case PinPadPrinterStatus.PrinterError: return new PrinterException(status, "Problemas com a impressora");
                case PinPadPrinterStatus.Overheating: return new PrinterOverheatingException();
                case PinPadPrinterStatus.UnfinishedPrinting: return new PrinterException(status, "Impressão não terminada");
                case PinPadPrinterStatus.LackOfFont: return new PrinterException(status, "Fonte de texto não disponível");
                case PinPadPrinterStatus.PackageTooLong: return new PrinterException(status, "Informação para imprimir muito grande.");
                default: return new PrinterException(status, "Código de erro desconhecido: " + status);
            }
        }
    }
}
