using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SuggestionSlot : MonoBehaviour
{
    [SerializeField] private Image _previewImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _departmentText;

    public void SetInfo(Chief chief)
    {
        _previewImage.sprite = chief.Preview;
        _nameText.text = chief.Name;
        _departmentText.text = chief.Department;
    }
}
