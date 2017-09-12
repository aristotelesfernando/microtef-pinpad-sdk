using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.Utilities;

namespace Pinpad.Sdk.Utilities
{
    /// <summary>
    /// Set of keys factory. 
    /// Necessary because different pinpads use different sets of navigation keys.
    /// </summary>
    internal static class DataPickerKeysFactory
    {
        /// <summary>
        /// Verifone Vx 820
        /// </summary>
        internal const string Verifone = "VERIFONE", Vx820 = "VX820";
        /// <summary>
        /// Gertec PPC920
        /// </summary>
        internal const string Gertec = "GERTEC", Ppc920 = "PPC920";
        /// <summary>
        /// Ingenico iPP320
        /// </summary>
        internal const string Ingenico = "INGENICO", Ipp320 = "IPP320";
        /// <summary>
        /// Gertec Mobi Pin 10
        /// </summary>
        internal const string MobiPin10 = "MOBI PIN 10";

        /// <summary>
        /// Create a <see cref="DataPickerKeys"/> based on the pin pad information.
        /// </summary>
        /// <param name="infos">Pinpad informarions.</param>
        /// <returns>Corresponding <see cref="DataPickerKeys"/> to pinpad.</returns>
        internal static DataPickerKeys GetUpAndDownKeys (this IPinpadInfos infos)
        {
            if (infos.ManufacturerName != null && infos.Model != null)
            {
                // Gertec PPC920
                if (infos.ManufacturerName.ToUpper().Contains(Gertec) && infos.Model.ToUpper().Contains(Ppc920))
                {
                    return new DataPickerKeys { UpKey = PinpadKeyCode.Function3, DownKey = PinpadKeyCode.Function4 };
                }
                // Verifone Vx 820
                else if (infos.ManufacturerName.ToUpper().Contains(Verifone) && infos.Model.ToUpper().Contains(Vx820))
                {
                    return new DataPickerKeys { UpKey = PinpadKeyCode.Function1, DownKey = PinpadKeyCode.Function3 };
                }
                // Ingenico iPP320PPC920
                else if (infos.ManufacturerName.ToUpper().Contains(Ingenico) && infos.Model.ToUpper().Contains(Ipp320))
                {
                    return new DataPickerKeys { UpKey = PinpadKeyCode.Function3, DownKey = PinpadKeyCode.Function2 };
                }
                // Gertec MOBI PIN 10
                else if (infos.ManufacturerName.ToUpper().Contains(Gertec) && infos.Model.ToUpper().Contains(MobiPin10))
                {
                    return new DataPickerKeys { UpKey = PinpadKeyCode.MobiPinUp, DownKey = PinpadKeyCode.MobiPinDown };
                }
            }

            return new DataPickerKeys(); // Gertec use by default.
        }
    }
}
