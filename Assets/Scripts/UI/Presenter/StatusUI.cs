using Extension;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nicknameText;
    [SerializeField] private TextMeshProUGUI[] _resourceText;

    private void Start()
    {
        if (_resourceText.Length != MoneySO.Length)
        {
            Debug.LogError("Error (StatusUI.Awake) : _resourceText.Length != _assets.Length");
            return;
        }

        _nicknameText.text = UserInfoSO.instance.nickname;

        for (int i = 0; i < MoneySO.Length; i++)
            UpdateUI(MoneySO.Get(i));
    }

    public void OnEnable()
    {
        Observe(true);
    }

    public void OnDisable()
    {
        Observe(false);
    }

    public void Observe(bool value)
    {
        if (value)
        {
            for (int i = 0; i < MoneySO.Length; i++)
                MoneySO.Get(i).onValueChanged += UpdateUI;
        }
        else
        {
            for (int i = 0; i < MoneySO.Length; i++)
                MoneySO.Get(i).onValueChanged -= UpdateUI;
        }
    }

    private void UpdateUI(MoneySO money)
    {
        // Effect

        _resourceText[(int)money.Type].text = $"{money.Amount}";
    }
}
