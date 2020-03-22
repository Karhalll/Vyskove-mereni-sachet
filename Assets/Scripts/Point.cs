using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using VZT.Utillities;
using GameDevTV.Saving;

public class Point : MonoBehaviour
{
    [SerializeField] Button usePoint = null;
    [SerializeField] Text pointNameText = null;
    [SerializeField] Text pointValueText = null;
    [SerializeField] EditPanel editPanel = null;
    [SerializeField] Button editButton = null;
    [SerializeField] Button deleteButton = null;
    [SerializeField] Button saveEditButton = null;
    [SerializeField] Button discardEditButton = null;

    string _pointName;
    public string pointName 
    {
        get { return _pointName; }
        set
        {
            _pointName = value;
            pointNameText.text = value;
        }
    }
    
    float _pointValue;
    public float pointValue
    {
        get { return _pointValue; }
        set
        {
            _pointValue = value;
            pointValueText.text = value.ToString("##0.00");
        }
    }

    private void Awake() 
    {
        editPanel.gameObject.SetActive(false);
        EditButtonsActive(false);
        pointName = ((Text)editPanel.editName.placeholder).text;
        pointValue = InputFieldConvertor.TextToFloat(editPanel.editValue);
    }

    // functions used by buttons START
    public void SendSavedPoint()
    {
        GetComponentInParent<PointToUse>().UseSavedPoint(pointValueText);
    }

    public void EditPoint()
    {
        InEditMode(true);
    }

    public void DeletePoint()
    {
        FindObjectOfType<SavingSystem>().SaveAfterDelay("save", 0.1f);
        Destroy(gameObject);
    }

    public void SaveChanges()
    {
        pointName = editPanel.editName.text;
        pointValue = InputFieldConvertor.TextToFloat(editPanel.editValue);

        FindObjectOfType<SavingSystem>().Save("save");

        InEditMode(false);
    }

    public void DiscardChanges()
    {
        InEditMode(false);
    }
    // functions used by buttons END

    private void InEditMode(bool state)
    {
        editPanel.gameObject.SetActive(state);

        editPanel.editName.text = _pointName;
        editPanel.editValue.text = _pointValue.ToString("##0.00");

        ButtonsActive(!state);
        EditButtonsActive(state);
        usePoint.gameObject.SetActive(!state);
    }

    private void ButtonsActive(bool state)
    {
        SetObjectActive(editButton, state);
        SetObjectActive(deleteButton, state);
    }

    private void EditButtonsActive(bool state)
    {
        SetObjectActive(saveEditButton, state);
        SetObjectActive(discardEditButton, state);
    }

    private void SetObjectActive(Button button, bool state)
    {
        button.gameObject.SetActive(state);
    }
}
