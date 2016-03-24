using System;

namespace Pinpad.Core.Rebuild
{
	public class XptoResponse : IPinpadResponse
	{
		public int ResponseNumber
		{
			get
			{
				return 10;
			}
		}
		public Enum ResponseCode
		{
			get
			{
				if (Enum.IsDefined(typeof(XptoResponseCode), this.ResponseNumber) == true)
				{
					return (XptoResponseCode) this.ResponseNumber;
				}

				return XptoResponseCode.Undefined;
			}
		}
	}
}
