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

    private void Start()
    {
        _upgradeButton.onClick.AddListener(() => _salesTeam.Upgrade());
    }

    private void OnEnable()
    {
        Observe(true);
    }

    private void OnDisable()
    {
        Observe(false);
    }

    public void SetInfo(SalesTeam salesTeam)
    {
        Observe(false);
        _salesTeam = salesTeam;
        Observe(true);

        UpdateUI(salesTeam);
    }

    private void Observe(bool value)
    {
        if (_salesTeam == null) return;

        if (value)
        {
            _salesTeam.onValueChanged += UpdateUI;
            _salesTeam.onProgressValueChanged += UpdateProgressUI;
        }
        else
        {
            _salesTeam.onValueChanged -= UpdateUI;
            _salesTeam.onProgressValueChanged -= UpdateProgressUI;
        }
    }

    private void UpdateUI(SalesTeam salesTeam)
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

    private void UpdateProgressUI(float progress)
    {
        _progressFillImage.fillAmount = progress;
    }
}
