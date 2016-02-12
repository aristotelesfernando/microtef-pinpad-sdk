using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pinpad.Sdk.Model
{
	public class PinpadInfo
	{
		private PinPadSDK.PinPad.PinPadInfos _rawInfo;

		public string ManufacturerName 
		{
			get { return this._rawInfo.ManufacturerName.Trim(); }
		}
		public string Model {
			get { return this._rawInfo.ModelVersion.Trim(); } 
		}
		public string Specifications 
		{
			get { return this._rawInfo.SpecificationVersion.Trim(); } 
		}
		public string SerialNumber 
		{
			get { return this._rawInfo.SerialNumber.Trim(); }
		}
		public bool IsContactless 
		{
			get { return this._rawInfo.ContactlessSupported; }
		}
		public string OperatingSystemVersion 
		{
			get { return this._rawInfo.OperationalSystemVersion.Trim(); }
		}
		public string ManufacturerVersion 
		{
			get { return this._rawInfo.ManufacturedVersion.Trim(); }
		}

		public PinpadInfo(PinPadSDK.PinPad.PinPadInfos rawInfo)
		{
			this._rawInfo = rawInfo;
		}
	}
}
