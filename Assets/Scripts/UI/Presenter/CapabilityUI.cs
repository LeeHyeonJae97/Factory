using Extension;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CapabilityUI : MonoBehaviour, IObserver
{
    [SerializeField] private TextMeshProUGUI[] _nameTexts;
    [SerializeField] private TextMeshProUGUI[] _levelTexts;
    [SerializeField] private Image[] _expFillImages;

    private void Start()
    {
        if (_nameTexts.Length != CapabilitySO.Length || _levelTexts.Length != CapabilitySO.Length || _expFillImages.Length != CapabilitySO.Length)
        {
            Debug.LogError("Error (AssetUI.Awake) : check ui element or capabilities");
            return;
        }

        for (int i = 0; i < CapabilitySO.Length; i++)
            UpdateUI(CapabilitySO.Get(i));
    }

    private void OnEnable()
    {
        Observe(true);
    }

    private void OnDisable()
    {
        Observe(false);
    }

    public void Observe(bool value)
    {
        if (value)
        {
            for (int i = 0; i < CapabilitySO.Length; i++)
                CapabilitySO.Get(i).onValueChanged += UpdateUI;
        }
        else
        {
            for (int i = 0; i < CapabilitySO.Length; i++)
                CapabilitySO.Get(i).onValueChanged -= UpdateUI;
        }
    }

    private void UpdateUI(CapabilitySO capability)
    {
        _nameTexts[(int)capability.Type].text = $"{capability.Name}";
        _levelTexts[(int)capability.Type].text = $"Level {capability.Level}";
        _expFillImages[(int)capability.Type].fillAmount = (float)capability.Exp / capability.MaxExp;
    }
}
