using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SalesTeamSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _inputText;
    [SerializeField] private TextMeshProUGUI _outputText;
    [SerializeField] private TextMeshProUGUI _leadTimeText;
    [SerializeField] private TextMeshProUGUI _unlockConditionText;
    [SerializeField] private TextMeshProUGUI _upgradeCostText;
    [SerializeField] private Image _progressFillImage;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private GameObject _unlockedCoverImage;

    private SalesTeam _salesTeam;

    public void Init(SalesTeam salesTeam)
    {
        _salesTeam = salesTeam;
        salesTeam.onValueChanged += UpdateUI;
        salesTeam.onProgressValueChanged += UpdateProgressUI;
        _upgradeButton.onClick.AddListener(Upgrade);
        UpdateUI(salesTeam);
    }

    private void OnEnable()
    {
        if (_salesTeam != null)
        {
            _salesTeam.onValueChanged += UpdateUI;
            _salesTeam.onProgressValueChanged += UpdateProgressUI;
        }
    }

    private void OnDisable()
    {
        _salesTeam.onValueChanged -= UpdateUI;
        _salesTeam.onProgressValueChanged -= UpdateProgressUI;
    }

    public void UpdateUI(SalesTeam salesTeam)
    {
        if (salesTeam.Level == 0)
        {
            _unlockedCoverImage.SetActive(true);
        }
        else
        {
            _nameText.text = salesTeam.Name;
            _inputText.text = $"Input Amount : {salesTeam.Input.Amount}";
            _outputText.text = $"Max Output Amount : {salesTeam.Input.Amount * salesTeam.Input.Asset.Price}";
            _leadTimeText.text = $"Lead Time : {salesTeam.LeadTime}";
            _unlockConditionText.text = $"Unlock Condition : {salesTeam.UnlockCondition.Amount}";
            _upgradeCostText.text = $"Upgrade Cost : {salesTeam.UpgradeCost.Amount}";
            _backgroundImage.sprite = salesTeam.Background;

            if (_unlockedCoverImage.activeInHierarchy) _unlockedCoverImage.SetActive(false);
        }
    }

    public void UpdateProgressUI(float progress)
    {
        _progressFillImage.fillAmount = progress;
    }

    public void Upgrade()
    {
        _salesTeam.Upgrade();
    }
}
