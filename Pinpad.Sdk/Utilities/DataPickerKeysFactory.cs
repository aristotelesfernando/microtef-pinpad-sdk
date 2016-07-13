using Pinpad.Sdk.Model;
using System;

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
        internal static DataPickerKeys Create(IPinpadInfos infos)
        {
            string verifone = "VERIFONE", vx820 = "VX820";
            string gertec = "GERTEC", ppc920 = "PPC920";
            string ingenico = "INGENICO", ipp320 = "IPP320";

            if (infos.ManufacturerName != null && infos.Model != null)
            {
                // Gertec PPC920
                if (infos.ManufacturerName.ToUpper().Contains(gertec) && infos.Model.ToUpper().Contains(ppc920))
                {
                    return new DataPickerKeys(PinpadKeyCode.Function3, PinpadKeyCode.Function4);
                }
                // Verifone Vx 820
                else if (infos.ManufacturerName.ToUpper().Contains(verifone) && infos.Model.ToUpper().Contains(vx820))
                {
                    return new DataPickerKeys(PinpadKeyCode.Function1, PinpadKeyCode.Function3);
                }
                // Ingenico iPP320
                else if (infos.ManufacturerName.ToUpper().Contains(ingenico) && infos.Model.ToUpper().Contains(ipp320))
                {
                    return new DataPickerKeys(PinpadKeyCode.Function3, PinpadKeyCode.Function2);
                }
            }

            return new DataPickerKeys(); // Gertec use by default.
        }
        /// <summary>
        /// Create a <see cref="DataPickerKeys"/> based in key codes passed.
        /// </summary>
        /// <param name="up"><see cref="PinpadKeyCode"/> to UP in navegation menu.</param>
        /// <param name="down"><see cref="PinpadKeyCode"/> to DOWN in navegation menu.</param>
        /// <returns><see cref="DataPickerKeys"/> with this up and down key codes.</returns>
        internal static DataPickerKeys Create(PinpadKeyCode up, PinpadKeyCode down)
        {
            return new DataPickerKeys(up, down);
        }
    }
}
