using System.Globalization;
using UnityEngine.UI;

namespace VZT.Utillities
{
    public static class InputFieldConvertor
    {
        static CultureInfo culture = CultureInfo.CreateSpecificCulture("en-EN");

        public static float TextToFloat(InputField inputField)
        {
            if (inputField.text != "")
            {
                return float.Parse(inputField.text);
            }
            else
            {
                return float.Parse(((Text) inputField.placeholder).text, culture);
            }
        }

        public static float TextToFloat(Text inputFieldTextComponent)
        {
            if (inputFieldTextComponent.text != "")
            {
                return float.Parse(inputFieldTextComponent.text);
            }
            else
            {
                return 0f;
            }
        }
    }
}