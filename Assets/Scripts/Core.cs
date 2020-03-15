using System;
using UnityEngine;
using UnityEngine.UI;

namespace VZT.Core
{
    public class Core : MonoBehaviour
    {
        [SerializeField] InputField inputFieldK = null;
        [SerializeField] InputField inputFieldD = null;

        [SerializeField] InputField inputFieldT = null;
        [SerializeField] InputField inputFieldY = null;

        [SerializeField] InputField inputFieldQ = null;
        [SerializeField] InputField inputFieldL = null;

        [SerializeField] Text vysledekDmin = null;

        [SerializeField] Text vysledekV = null;
        [SerializeField] Text vysledekI = null;
        [SerializeField] Text vysledekZ = null;

        decimal K_mm = 0.01m;           // absolutni drsnost potrubi
        decimal D_mm = 110m;            // vnejsi prumer

        decimal T_mm = 3.2m;            // tloustka steny
        decimal Y_mm = 0.5m;            // odchylka

        decimal Q_LperS = 9.5m;         // prutok
        decimal L_m = 150m;             // delka potrubi

        decimal D_min = 0m;

        private void Awake() 
        {
            
        }

        private void Start()
        {
            // K_mm = Convert.ToDecimal(inputFieldK.);
            // D_mm = Convert.ToDecimal(inputFieldD.placeholder);

            // T_mm = Convert.ToDecimal(inputFieldT.placeholder);
            // Y_mm = Convert.ToDecimal(inputFieldY.placeholder);

            // Q_LperS = Convert.ToDecimal(inputFieldQ.placeholder);
            // L_m = Convert.ToDecimal(inputFieldL.placeholder);

            // print(K_mm);
            // print(D_mm);

            // print(T_mm);
            // print(Y_mm);

            // print(Q_LperS);
            // print(L_m);

            Calculate();   
        }

        public void Recalculate()
        {
            if (inputFieldK.text != String.Empty) K_mm = Convert.ToDecimal(inputFieldK.text);
            if (inputFieldD.text != String.Empty) D_mm = Convert.ToDecimal(inputFieldD.text);

            if (inputFieldT.text != String.Empty) T_mm = Convert.ToDecimal(inputFieldT.text);
            if (inputFieldY.text != String.Empty) Y_mm = Convert.ToDecimal(inputFieldY.text);

            if (inputFieldQ.text != String.Empty) Q_LperS = Convert.ToDecimal(inputFieldQ.text);
            if (inputFieldL.text != String.Empty) L_m = Convert.ToDecimal(inputFieldL.text);

            Calculate();
        }

        private void Calculate()
        {
            decimal K = DivideBy1000(K_mm);

            D_min = DMinCalculation(D_mm, T_mm, Y_mm);

            decimal D = DivideBy1000(D_min);
            decimal Q = DivideBy1000(Q_LperS);

            decimal S = (decimal)Mathf.PI * (decimal)Mathf.Pow((float)D, 2f) / 4;
            decimal V = Q / S;
            decimal C = V * D / (1.3101m * (decimal)Mathf.Pow(10f, -6f));

            GoTo300(K, D, V, C);
        }

        private void GoTo300(decimal K, decimal D, decimal V, decimal C)
        {
            decimal L = (decimal)Mathf.Pow((1f / Mathf.Pow((-2f * Mathf.Log10((float)K / (float)D) + 1.13874f), 8f) + 0.01f / (float)C), 0.25f);
            decimal M = 0m;
            decimal Z = 0.000003m;

            GoTo500(K, D, V, C, ref L, ref M, ref Z);
        }

        private void GoTo500(decimal K, decimal D, decimal V, decimal C, ref decimal L, ref decimal M, ref decimal Z)
        {
            decimal U = 1m;
            do
            {
                decimal A = 1m / (decimal)Mathf.Pow((float)L, 1f / 2f);
                decimal B = -2m * (decimal)Mathf.Log10(((float)K / (3.71f * (float)D)) + (2.51f / ((float)C * Mathf.Pow((float)L, 1f / 2f))));
                U = (decimal)Mathf.Abs((float)A - (float)B);

                if (U < 0.0003m)
                {
                    GoTo100(D, V, L);
                }
                else
                {
                    decimal N = U;

                    if (M < N)
                    {
                        Z = -Z;
                    }

                    M = N;
                    L = L + Z;
                }
                
            }while (U > 0.0003m);
            
        }

        private void GoTo100(decimal D, decimal V, decimal L)
        {
            vysledekDmin.text = String.Format("{0:# ##0.#}", D_min);

            decimal I = (L / D) * ((decimal)Mathf.Pow((float)V, 2f) / (2m * 9.81m));
            vysledekV.text = String.Format("{0:0.###}", V);

            decimal I_final = I * 1000m;
            vysledekI.text = String.Format("{0:0.###}", I_final);

            decimal Z_final = L_m * I;
            vysledekZ.text = String.Format("{0:0.###}", Z_final);
        }

        private decimal DivideBy1000(decimal x)
        {
            return x/1000;
        }

        private decimal DMinCalculation(decimal D, decimal T, decimal Y)
        {
            return D - 2*T - 2*Y;
        }
    }
}
