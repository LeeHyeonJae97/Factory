using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AssetUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _resourceText;
    [SerializeField] private AssetSO[] _assets;

    private void OnEnable()
    {
        for (int i = 0; i < _assets.Length; i++)
            _assets[i].onValueChanged += UpdateUI;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _assets.Length; i++)
            _assets[i].onValueChanged -= UpdateUI;
    }

    private void UpdateUI(AssetType type, int amount)
    {
        // Effect

        _resourceText[(int)type].text = $"{amount}";
    }
}
