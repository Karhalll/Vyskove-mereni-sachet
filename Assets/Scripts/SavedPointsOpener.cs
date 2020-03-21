using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameDevTV.Saving;

public class SavedPointsOpener : MonoBehaviour
{
    [SerializeField] Canvas savePointsCanvas = null;

    private void Start() 
    {
        savePointsCanvas.enabled = false;
    }

    public void OpenSavePointsCanvas()
    {
        GetComponent<SavingSystem>().LoadFromFile("save");
        savePointsCanvas.enabled = true;

    }

    public void CloseSavePointsCanvas()
    {
        GetComponent<SavingSystem>().Save("save");
        savePointsCanvas.enabled = false;
    }
}
