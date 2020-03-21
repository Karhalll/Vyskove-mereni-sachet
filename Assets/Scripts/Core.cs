using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

using VZT.Utillities;

namespace VZT.Core
{
    public class Core : MonoBehaviour
    {
        [SerializeField] InputField fix = null;
        [SerializeField] InputField mereniFix = null;

        [SerializeField] InputField mereniNovy = null;
        [SerializeField] Text vysledekText = null;

        float fixFloat;
        float mereniFixFloat;
        float mereniNovyFloat;

        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-EN");

        private void Start() 
        {
            Recalculate();
        }

        public void Recalculate()
        {
            GetMesurementValues();

            float vysledek = fixFloat + mereniFixFloat - mereniNovyFloat;

            float vyseldek2DP = Mathf.Round(vysledek * 1000f) / 1000f;

            vysledekText.text = vyseldek2DP.ToString("##0.00#");
        }

        private void GetMesurementValues()
        {
            fixFloat = InputFieldConvertor.TextToFloat(fix);
            mereniFixFloat = InputFieldConvertor.TextToFloat(mereniFix);
            mereniNovyFloat = InputFieldConvertor.TextToFloat(mereniNovy);
        }
    }
}