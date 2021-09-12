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

    public void Init(Farm farm)
    {
        _farm = farm;
        farm.onValueChanged += UpdateUI;
        farm.onProgressValueChanged += UpdateProgressUI;
        _upgradeButton.onClick.AddListener(Upgrade);
        UpdateUI(farm);
    }

    private void OnEnable()
    {
        if (_farm != null)
        {
            _farm.onValueChanged += UpdateUI;
            _farm.onProgressValueChanged += UpdateProgressUI;
        }
    }

    private void OnDisable()
    {
        _farm.onValueChanged -= UpdateUI;
        _farm.onProgressValueChanged -= UpdateProgressUI;
    }

    public void UpdateUI(Farm farm)
    {
        if (farm.Level == 0)
        {
            _unlockedCoverImage.SetActive(true);
        }
        else
        {
            _nameText.text = farm.Name;
            _outputText.text = $"Output Amount : {farm.Output.Amount}";
            _leadTimeText.text = $"Lead Time : {farm.LeadTime}";
            _unlockConditionText.text = $"Unlock Condition : {farm.UnlockCondition.Amount}";
            _upgradeCostText.text = $"Upgrade Cost : {farm.UpgradeCost.Amount}";
            _backgroundImage.sprite = farm.Background;

            if (_unlockedCoverImage.activeInHierarchy) _unlockedCoverImage.SetActive(false);
        }
    }

    public void UpdateProgressUI(float progress)
    {
        _progressFillImage.fillAmount = progress;
    }

    public void Upgrade()
    {
        _farm.Upgrade();
    }
}
