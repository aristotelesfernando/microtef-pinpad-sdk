using System;

namespace Pinpad.Core.Rebuild
{
	public class AbcdResponse : IPinpadResponse
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
				if (Enum.IsDefined(typeof(AbcdResponseCode), this.ResponseNumber) == true)
				{
					return (AbcdResponseCode) this.ResponseNumber;
				}

				return AbcdResponseCode.Undefined;
			}
		}
	}
}
