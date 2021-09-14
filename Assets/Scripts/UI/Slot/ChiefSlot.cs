using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChiefSlot : MonoBehaviour, ISlot
{
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private Button _button;

    private Chief _chief;

    public void OnEnable()
    {
        Observe(true);
    }

    public void OnDisable()
    {
        Observe(false);
    }

    public void Init(UnityAction<Chief> open)
    {
        _button.onClick.AddListener(() => open?.Invoke(_chief));
    }

    public void SetInfo(Chief chief)
    {
        Observe(false);
        _chief = chief;
        Observe(true);

        UpdateUI(chief.Amount);
    }

    private void Observe(bool value)
    {
        if (_chief == null) return;

        if (value)
            _chief.onAmountValueChanged += UpdateUI;
        else
            _chief.onAmountValueChanged -= UpdateUI;
    }

    private void UpdateUI(int amount)
    {
        _amountText.text = $"{amount}";
    }
}
