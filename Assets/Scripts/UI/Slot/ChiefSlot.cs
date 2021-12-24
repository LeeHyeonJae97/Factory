using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChiefSlot : MonoBehaviour, IPresenter<ChiefSO>, IObserver
{
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private GameObject _coverImage;
    [SerializeField] private Button _button;
    [SerializeField] private ChiefUI _chiefUI;

    private ChiefSO _chief;

    private void Start()
    {
        _button.onClick.AddListener(() =>
        {
            _chiefUI.SetInfo(_chief);
            _chiefUI.SetActive(true);
        });
    }

    private void OnEnable()
    {
        Observe(true);
    }

    private void OnDisable()
    {
        Observe(false);
    }

    public void SetInfo(ChiefSO chief)
    {
        _chief = chief;
    }

    public void Observe(bool value)
    {
        if (_chief == null) return;

        if (value)
        {
            _chief.onAmountValueChanged += UpdateUI;
            UpdateUI(_chief.Amount);
        }            
        else
        {
            _chief.onAmountValueChanged -= UpdateUI;
        }            
    }

    private void UpdateUI(int amount)
    {
        _amountText.text = $"{amount}";
        _coverImage.SetActive(amount == 0);
    }
}
