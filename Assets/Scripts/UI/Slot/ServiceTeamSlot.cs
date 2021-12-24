using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServiceTeamSlot : MonoBehaviour, IPresenter<ServiceTeamSO>, IObserver
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

    private ServiceTeamSO _serviceTeam;

    private void Start()
    {
        _upgradeButton.onClick.AddListener(() => _serviceTeam.Upgrade());
    }

    private void OnEnable()
    {
        Observe(true);
    }

    private void OnDisable()
    {
        Observe(false);
    }

    public void SetInfo(ServiceTeamSO serviceTeam)
    {
        _serviceTeam = serviceTeam;
    }

    public void Observe(bool value)
    {
        if (_serviceTeam == null) return;

        if (value)
        {
            UpdateUI(_serviceTeam);

            _serviceTeam.onValueChanged += UpdateUI;
            _serviceTeam.onProgressValueChanged += UpdateProgressUI;
        }
        else
        {
            _serviceTeam.onValueChanged -= UpdateUI;
            _serviceTeam.onProgressValueChanged -= UpdateProgressUI;
        }
    }

    private void UpdateUI(ServiceTeamSO serviceTeam)
    {
        _nameText.text = serviceTeam.Name;
        _outputText.text = $"Output Amount : {serviceTeam.OutputAmount}";
        _leadTimeText.text = $"Lead Time : {serviceTeam.LeadTime}";
        _unlockConditionText.text = $"Unlock Condition : {serviceTeam.UnlockConditionAmount}";
        _upgradeCostText.text = $"Upgrade Cost : {serviceTeam.UpgradeCost}";
        _backgroundImage.sprite = serviceTeam.Background;
        _unlockedCoverImage.SetActive(serviceTeam.Level == 0);
        _progressFillImage.fillAmount = serviceTeam.Progress;
    }

    private void UpdateProgressUI(float progress)
    {
        _progressFillImage.fillAmount = progress;
    }
}
