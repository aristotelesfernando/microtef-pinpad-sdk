using Pinpad.Sdk.Model.Exceptions;
using Pinpad.Sdk.Model.TypeCode;
using System;

namespace Pinpad.Sdk.Utilities
{
	/// <summary>
	/// Factory methods for printer exceptions
	/// </summary>
	internal class PrinterExceptionFactory 
	{
		/// <summary>
		/// Creates a Printer Exception based on a Printer Status
		/// </summary>
		/// <param name="status">Printer Status</param>
		/// <returns>PrinterException</returns>
		public PrinterException Create(PinpadPrinterStatus status) 
		{
			switch (status)
			{
				case PinpadPrinterStatus.Undefined: 
					throw new InvalidOperationException("Attempt to create a PrinterException without a Printer Status");

				case PinpadPrinterStatus.Ready:
					//Sucesso
					return null; 

				case PinpadPrinterStatus.Busy: 
					return new PrinterBusyException();

				case PinpadPrinterStatus.OutOfPaper: 
					return new PrinterOutOfPaperException();

				case PinpadPrinterStatus.InvalidFormat: 
					return new PrinterException(status, "Formato da informação para imprimir inválido");

				case PinpadPrinterStatus.PrinterError: 
					return new PrinterException(status, "Problemas com a impressora");

				case PinpadPrinterStatus.Overheating: 
					return new PrinterOverheatingException();

				case PinpadPrinterStatus.UnfinishedPrinting: 
					return new PrinterException(status, "Impressão não terminada");

				case PinpadPrinterStatus.LackOfFont: 
					return new PrinterException(status, "Fonte de texto não disponível");

				case PinpadPrinterStatus.PackageTooLong: 
					return new PrinterException(status, "Informação para imprimir muito grande.");
				
				default: 
					return new PrinterException(status, "Código de erro desconhecido: " + status);
			}
		}
	}
}
