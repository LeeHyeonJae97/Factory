using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FactorySlot : MonoBehaviour
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

    private Factory _factory;

    public void Init(Factory factory)
    {
        _factory = factory;
        factory.onValueChanged += UpdateUI;
        factory.onProgressValueChanged += UpdateProgressUI;
        _upgradeButton.onClick.AddListener(Upgrade);
        UpdateUI(factory);
    }

    private void OnEnable()
    {
        if (_factory != null)
        {
            _factory.onValueChanged += UpdateUI;
            _factory.onProgressValueChanged += UpdateProgressUI;
        }
    }

    private void OnDisable()
    {
        _factory.onValueChanged -= UpdateUI;
        _factory.onProgressValueChanged -= UpdateProgressUI;
    }

    public void UpdateUI(Factory factory)
    {
        if (factory.Level == 0)
        {
            _unlockedCoverImage.SetActive(true);
        }
        else
        {
            _nameText.text = factory.Name;
            for (int i = 0; i < _inputTexts.Length; i++)
            {
                if (i < factory.Inputs.Length)
                {
                    _inputTexts[i].gameObject.SetActive(true);
                    _inputTexts[i].text = $"Input Amount : {factory.Inputs[i].Amount}";
                }
                else
                {
                    _inputTexts[i].gameObject.SetActive(false);
                }
            }
            _outputText.text = $"Output Amount : {factory.Output.Amount}";
            _leadTimeText.text = $"Lead Time : {factory.LeadTime}";
            _unlockConditionText.text = $"Unlock Condition : {factory.UnlockCondition.Amount}";
            _upgradeCostText.text = $"Upgrade Cost : {factory.UpgradeCost.Amount}";
            _backgroundImage.sprite = factory.Background;

            if (_unlockedCoverImage.activeInHierarchy) _unlockedCoverImage.SetActive(false);
        }
    }

    public void UpdateProgressUI(float progress)
    {
        _progressFillImage.fillAmount = progress;
    }

    public void Upgrade()
    {
        _factory.Upgrade();
    }
}
