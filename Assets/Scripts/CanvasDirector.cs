using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasDirector : MonoBehaviour
{
    [SerializeField] Canvas mainCanvas = null;
    [SerializeField] Canvas mesureCanvas = null;

    InputField inputField = null;

    public void SwitchToRodMesurement(InputField inputField)
    {
        CanvasVisibility(mainCanvas, false);
        CanvasVisibility(mesureCanvas, true);
        this.inputField = inputField;
    }

    public void SwitchOffRodMesurement()
    {
        CanvasVisibility(mainCanvas, true);
        CanvasVisibility(mesureCanvas, false);
        inputField.text = Camera.main.transform.position.y.ToString("##0.00#");
    }

    private void CanvasVisibility(Canvas canvas, bool state)
    {
        canvas.enabled = state;
    }
}
