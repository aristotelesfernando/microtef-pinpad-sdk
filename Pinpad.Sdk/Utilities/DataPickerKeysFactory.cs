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
        /// Create a <see cref="DataPickerKeys"/> based on the pin pad information.
        /// </summary>
        /// <param name="infos">Pinpad informarions.</param>
        /// <returns>Corresponding <see cref="DataPickerKeys"/> to pinpad.</returns>
        internal static DataPickerKeys GetUpAndDownKeys (this IPinpadInfos infos)
        {
            string verifone = "VERIFONE", vx820 = "VX820";
            string gertec = "GERTEC", ppc920 = "PPC920";
            string ingenico = "INGENICO", ipp320 = "IPP320";

            if (infos.ManufacturerName != null && infos.Model != null)
            {
                // Gertec PPC920
                if (infos.ManufacturerName.ToUpper().Contains(gertec) && infos.Model.ToUpper().Contains(ppc920))
                {
                    return new DataPickerKeys { UpKey = PinpadKeyCode.Function3, DownKey = PinpadKeyCode.Function4 };
                }
                // Verifone Vx 820
                else if (infos.ManufacturerName.ToUpper().Contains(verifone) && infos.Model.ToUpper().Contains(vx820))
                {
                    return new DataPickerKeys { UpKey = PinpadKeyCode.Function1, DownKey = PinpadKeyCode.Function3 };
                }
                // Ingenico iPP320
                else if (infos.ManufacturerName.ToUpper().Contains(ingenico) && infos.Model.ToUpper().Contains(ipp320))
                {
                    return new DataPickerKeys { UpKey = PinpadKeyCode.Function3, DownKey = PinpadKeyCode.Function2 };
                }
            }

            return new DataPickerKeys(); // Gertec use by default.
        }
    }
}
