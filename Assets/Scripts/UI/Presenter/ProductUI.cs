using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ProductUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _resourceText;
    [SerializeField] private ProductSO[] _products;

    private void OnEnable()
    {
        for (int i = 0; i < _products.Length; i++)
            _products[i].onValueChanged += UpdateUI;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _products.Length; i++)
            _products[i].onValueChanged -= UpdateUI;
    }

    private void UpdateUI(int id, int amount)
    {
        // Effect

        _resourceText[id].text = $"{amount}";
    }
}
