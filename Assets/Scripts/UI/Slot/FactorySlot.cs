using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FactorySlot : MonoBehaviour, IPresenter<FactorySO>, IObserver
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI[] _inputTexts;
    [SerializeField] private TextMeshProUGUI _outputText;
    [SerializeField] private TextMeshProUGUI _leadTimeText;
    [SerializeField] private TextMeshProUGUI _unlockConditionText;
    [SerializeField] private TextMeshProUGUI _upgradeCostText;
    [SerializeField] private Image _progressFillImage;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private GameObject _unlockedCoverImage;

    private FactorySO _factory;

    private void Start()
    {
        _upgradeButton.onClick.AddListener(() => _factory.Upgrade());
    }

    private void OnEnable()
    {
        Observe(true);
    }

    private void OnDisable()
    {
        Observe(false);
    }

    public void SetInfo(FactorySO factory)
    {
        _factory = factory;
    }

    public void Observe(bool value)
    {
        if (_factory == null) return;

        if (value)
        {
            UpdateUI(_factory);

            _factory.onValueChanged += UpdateUI;
            _factory.onProgressValueChanged += UpdateProgressUI;
        }
        else
        {
            _factory.onValueChanged -= UpdateUI;
            _factory.onProgressValueChanged -= UpdateProgressUI;
        }
    }

    private void UpdateUI(FactorySO factory)
    {
        _nameText.text = factory.Name;
        for (int i = 0; i < _inputTexts.Length; i++)
        {
            if (i < factory.InputAmounts.Length)
            {
                _inputTexts[i].gameObject.SetActive(true);
                _inputTexts[i].text = $"Input Amount : {factory.InputAmounts[i]}";
            }
            else
            {
                _inputTexts[i].gameObject.SetActive(false);
            }
        }
        _outputText.text = $"Output Amount : {factory.OutputAmount}";
        _leadTimeText.text = $"Lead Time : {factory.LeadTime}";
        _unlockConditionText.text = $"Unlock Condition : {factory.UnlockConditionAmount}";
        _upgradeCostText.text = $"Upgrade Cost : {factory.UpgradeCost}";
        _backgroundImage.sprite = factory.Background;
        _unlockedCoverImage.SetActive(factory.Level == 0);
        _progressFillImage.fillAmount = factory.Progress;
    }

    private void UpdateProgressUI(float progress)
    {
        _progressFillImage.fillAmount = progress;
    }
}
