using Extension;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChiefUI : MonoBehaviour
{
    [SerializeField] private Image _previewImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _buffText;
    [SerializeField] private TextMeshProUGUI _amountText;

    private Canvas _canvas;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

    public void Open(Chief chief)
    {
        _previewImage.sprite = chief.Preview;
        _nameText.text = chief.Name;
        _descriptionText.text = chief.Description;
        _buffText.text = $"{chief.Buff}";
        _amountText.text = $"{chief.Amount}";

        _canvas.SetActive(true);
    }

    public void Close()
    {
        _canvas.SetActive(false);
    }
}
