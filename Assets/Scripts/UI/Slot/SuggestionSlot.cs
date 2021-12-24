using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SuggestionSlot : MonoBehaviour, IPresenter<ChiefSO>
{
    [SerializeField] private Image _previewImage;
    [SerializeField] private TextMeshProUGUI _nameText;

    public void SetInfo(ChiefSO chief)
    {
        _previewImage.sprite = chief.Preview;
        _nameText.text = chief.Name;
    }
}
