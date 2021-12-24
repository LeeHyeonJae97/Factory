using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SalesTeamSlot : MonoBehaviour, IPresenter<SalesTeamSO>, IObserver
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

    private SalesTeamSO _salesTeam;

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

    public void SetInfo(SalesTeamSO salesTeam)
    {
        _salesTeam = salesTeam;
    }

    public void Observe(bool value)
    {
        if (_salesTeam == null) return;

        if (value)
        {
            UpdateUI(_salesTeam);

            _salesTeam.onValueChanged += UpdateUI;
            _salesTeam.onProgressValueChanged += UpdateProgressUI;
        }
        else
        {
            _salesTeam.onValueChanged -= UpdateUI;
            _salesTeam.onProgressValueChanged -= UpdateProgressUI;
        }
    }

    private void UpdateUI(SalesTeamSO salesTeam)
    {
        _nameText.text = salesTeam.Name;
        _inputText.text = $"Input Amount : {salesTeam.InputAmount}";
        _outputText.text = $"Max Output Amount : {salesTeam.InputAmount * salesTeam.Input.Price}";
        _leadTimeText.text = $"Lead Time : {salesTeam.LeadTime}";
        _unlockConditionText.text = $"Unlock Condition : {salesTeam.UnlockConditionAmount}";
        _upgradeCostText.text = $"Upgrade Cost : {salesTeam.UpgradeCost}";
        _backgroundImage.sprite = salesTeam.Background;
        _unlockedCoverImage.SetActive(salesTeam.Level == 0);
        _progressFillImage.fillAmount = salesTeam.Progress;
    }

    private void UpdateProgressUI(float progress)
    {
        _progressFillImage.fillAmount = progress;
    }
}
