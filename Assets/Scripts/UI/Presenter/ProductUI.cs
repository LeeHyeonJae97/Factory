using Extension;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ProductUI : MonoBehaviour, IObserver
{
    [SerializeField] private TextMeshProUGUI[] _resourceText;

    private void Start()
    {
        if (_resourceText.Length != ProductSO.Length)
        {
            Debug.LogError("Error (AssetUI.Awake) : _resourceText.Length != _products.Length");
            return;
        }

        for (int i = 0; i < ProductSO.Length; i++)
            UpdateUI(ProductSO.Get(i));
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
            for (int i = 0; i < ProductSO.Length; i++)
                ProductSO.Get(i).onValueChanged += UpdateUI;
        }
        else
        {
            for (int i = 0; i < ProductSO.Length; i++)
                ProductSO.Get(i).onValueChanged -= UpdateUI;
        }
    }

    private void UpdateUI(ProductSO product)
    {
        // Effect

        _resourceText[(int)product.Type].text = $"{product.Amount}";
    }
}
