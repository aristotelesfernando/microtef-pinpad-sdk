using System;
using System.Collections.Generic;

namespace Pinpad.Sdk.Commands.Context
{
	internal class DataPickerContext : IContext
	{
		public short CommandNameLength
		{
			get
			{
				return 0;
			}
		}

		public byte EndByte
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool HasToIncludeFirstByte
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public short IntegrityCodeLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public byte StartByte
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public short StatusLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public void FormatResponse (List<byte> response)
		{
			throw new NotImplementedException();
		}

		public byte [] GetIntegrityCode (byte [] data)
		{
			throw new NotImplementedException();
		}

		public List<byte> GetRequestBody (BaseCommand request)
		{
			return null;	
		}

		public bool IsIntegrityCodeValid (byte [] firstCode, byte [] secondCode)
		{
			throw new NotImplementedException();
		}
	}
}
