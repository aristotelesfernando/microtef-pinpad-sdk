using System;
using System.Collections.Generic;

namespace Pinpad.Sdk.Commands.Context
{
	/// <summary>
	/// Context of a command.
	/// </summary>
	public interface IContext
	{
		/// <summary>
		/// Byte indicating the beggining of a package.
		/// </summary>
		byte StartByte { get; }
		/// <summary>
		/// Byte indicating the end of a package.
		/// </summary>
		byte EndByte { get; }
		/// <summary>
		/// Command name fixed length.
		/// </summary>
		short CommandNameLength { get; }

		short StatusLength { get; }

		short IntegrityCodeLength { get; }

		bool HasToIncludeFirstByte { get; }

		/// <summary>
		/// Performs a hash of some kind under the original data. 
		/// Used to verify whether the block of data was transmitted without errors.
		/// (CheckSum, LRC...)
		/// </summary>
		/// <returns>Returns the hashed, used as integrity validation of a block of data.</returns>
		byte [] GetIntegrityCode (byte [] data);
		/// <summary>
		/// Get's the body of a command as a list of bytes, including integrity validation.
		/// </summary>
		/// <param name="request">The request to be turned into a list of bytes.</param>
		/// <returns>List of bytes ready to be sent to the pinpad.</returns>
		List<byte> GetRequestBody (BaseCommand request);
		void FormatResponse (List<byte> response);
		bool IsIntegrityCodeValid (byte [] firstCode, byte [] secondCode);
	}
}