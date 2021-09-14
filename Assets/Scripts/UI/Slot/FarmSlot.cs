using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class FarmSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _outputText;
    [SerializeField] private TextMeshProUGUI _leadTimeText;
    [SerializeField] private TextMeshProUGUI _unlockConditionText;
    [SerializeField] private TextMeshProUGUI _upgradeCostText;
    [SerializeField] private Image _progressFillImage;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private GameObject _unlockedCoverImage;

    private Farm _farm;

    private void Start()
    {
        _upgradeButton.onClick.AddListener(() => _farm.Upgrade());
    }

    private void OnEnable()
    {
        Observe(true);
    }

    private void OnDisable()
    {
        Observe(false);
    }

    public void SetInfo(Farm farm)
    {
        Observe(false);
        _farm = farm;
        Observe(true);

        UpdateUI(farm);
    }

    private void Observe(bool value)
    {
        if (_farm == null) return;

        if(value)
        {
            _farm.onValueChanged += UpdateUI;
            _farm.onProgressValueChanged += UpdateProgressUI;
        }
        else
        {
            _farm.onValueChanged -= UpdateUI;
            _farm.onProgressValueChanged -= UpdateProgressUI;
        }
    }

    private void UpdateUI(Farm farm)
    {
        if (farm.Level == 0)
        {
            _unlockedCoverImage.SetActive(true);
        }
        else
        {
            _nameText.text = farm.Name;
            _outputText.text = $"Output Amount : {farm.Output}";
            _leadTimeText.text = $"Lead Time : {farm.LeadTime}";
            _unlockConditionText.text = $"Unlock Condition : {farm.UnlockCondition.Amount}";
            _upgradeCostText.text = $"Upgrade Cost : {farm.UpgradeCost.Amount}";
            _backgroundImage.sprite = farm.Background;

            if (_unlockedCoverImage.activeInHierarchy) _unlockedCoverImage.SetActive(false);
        }
    }

    private void UpdateProgressUI(float progress)
    {
        _progressFillImage.fillAmount = progress;
    }
}
