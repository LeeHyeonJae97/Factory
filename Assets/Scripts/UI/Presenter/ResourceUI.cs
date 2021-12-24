using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using TigerForge;
using Extension;

public class ResourceUI : MonoBehaviour, IObserver
{
    [SerializeField] private TextMeshProUGUI[] _resourceText;

    private void Start()
    {
        if (_resourceText.Length != ResourceSO.Length)
        {
            Debug.LogError("Error (AssetUI.Awake) : _resourceText.Length != _assets.Length");
            return;
        }

        for (int i = 0; i < ResourceSO.Length; i++)
            UpdateUI(ResourceSO.Get(i));
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
            for (int i = 0; i < ResourceSO.Length; i++)
                ResourceSO.Get(i).onValueChanged += UpdateUI;
        }
        else
        {
            for (int i = 0; i < ResourceSO.Length; i++)
                ResourceSO.Get(i).onValueChanged -= UpdateUI;
        }
    }

    private void UpdateUI(ResourceSO resource)
    {
        // Effect

        _resourceText[(int)resource.Type].text = $"{resource.Amount}";
    }
}
