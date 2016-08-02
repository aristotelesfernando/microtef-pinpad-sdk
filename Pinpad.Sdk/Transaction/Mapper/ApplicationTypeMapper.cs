using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Model;

namespace Pinpad.Sdk.Transaction.Mapper
{
	internal static class ApplicationTypeMapper
	{
		public static CardType ToCardType (this ApplicationType appType)
		{
			switch (appType)
			{
				case ApplicationType.ContactlessEmv:
				case ApplicationType.IccEmv:
					return CardType.Emv;

				case ApplicationType.ContactlessMagneticStripe:
				case ApplicationType.MagneticStripe:
					return CardType.MagneticStripe;

				default:
					return CardType.Undefined;
			}
		} 
	}
}
