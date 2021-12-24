using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class FarmSlot : MonoBehaviour, IPresenter<FarmSO>, IObserver
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

    private FarmSO _farm;

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

    public void SetInfo(FarmSO farm)
    {
        _farm = farm;
    }

    public void Observe(bool value)
    {
        if (_farm == null) return;

        if (value)
        {
            UpdateUI(_farm);

            _farm.onValueChanged += UpdateUI;
            _farm.onProgressValueChanged += UpdateProgressUI;
        }
        else
        {
            _farm.onValueChanged -= UpdateUI;
            _farm.onProgressValueChanged -= UpdateProgressUI;
        }
    }

    private void UpdateUI(FarmSO farm)
    {
        _nameText.text = farm.Name;
        _outputText.text = $"Output Amount : {farm.OutputAmount}";
        _leadTimeText.text = $"Lead Time : {farm.LeadTime}";
        _unlockConditionText.text = $"Unlock Condition : {farm.UnlockConditionAmount}";
        _upgradeCostText.text = $"Upgrade Cost : {farm.UpgradeCost}";
        _backgroundImage.sprite = farm.Background;
        _unlockedCoverImage.SetActive(farm.Level == 0);
        _progressFillImage.fillAmount = farm.Progress;
    }

    private void UpdateProgressUI(float progress)
    {
        _progressFillImage.fillAmount = progress;
    }
}
