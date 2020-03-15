using System;
using UnityEngine;
using UnityEngine.UI;

namespace VZT.Core
{
    public class Core : MonoBehaviour
    {
        [SerializeField] InputField fix = null;
        [SerializeField] InputField mereniFix = null;

        [SerializeField] InputField mereniNovy = null;
        [SerializeField] Text vysledekText = null;

        public void Recalculate()
        {
            float fixFloat = (float)Convert.ToDecimal(fix.text);
            float mereniFixFloat = (float)Convert.ToDecimal(mereniFix.text);
            float mereniNovyFloat = (float)Convert.ToDecimal(mereniNovy.text);

            float vysledek = fixFloat + mereniFixFloat - mereniNovyFloat;

            float vyseldek2DP = Mathf.Round(vysledek * 100f) / 100f;

            vysledekText.text = vyseldek2DP.ToString("###.00");
        }
    }
}