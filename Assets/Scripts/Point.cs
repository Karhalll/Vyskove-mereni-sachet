using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using VZT.Utillities;

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

    string pointName = string.Empty;
    float pointValue = 0f;

    private void Awake() 
    {
        editPanel.gameObject.SetActive(false);
        EditButtonsActive(false);
        SetName(((Text)editPanel.editName.placeholder).text);
        SetValue(InputFieldConvertor.TextToFloat(editPanel.editValue));
    }

    private void Start() 
    {
    }

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
        Destroy(gameObject);
    }

    public void SaveChanges()
    {
        SetName(editPanel.editName.text);
        SetValue(InputFieldConvertor.TextToFloat(editPanel.editValue));

        InEditMode(false);
    }

    public void DiscardChanges()
    {
        InEditMode(false);
    }

    private void InEditMode(bool state)
    {
        editPanel.gameObject.SetActive(state);
        ButtonsActive(!state);
        EditButtonsActive(state);
        usePoint.gameObject.SetActive(!state);
    }

    private void ButtonsActive(bool state)
    {
        EditButtonVisible(state);
        DeleteButtonVisible(state);
    }

    private void EditButtonsActive(bool state)
    {
        saveEditButton.gameObject.SetActive(state);
        discardEditButton.gameObject.SetActive(state);
    }

    private void EditButtonVisible(bool switchState)
    {
        editButton.gameObject.SetActive(switchState);
    }

    private void DeleteButtonVisible(bool switchState)
    {
        deleteButton.gameObject.SetActive(switchState);
    }

    public string GetName()
    {
        return pointName;
    }

    public void SetName(string newName)
    {
        pointName = newName;
        pointNameText.text = newName;
    }

    public float GetValue()
    {
        return pointValue;
    }

    public void SetValue(float newValue)
    {
        pointValue = newValue;
        pointValueText.text = newValue.ToString("##0.00");
    }
}
