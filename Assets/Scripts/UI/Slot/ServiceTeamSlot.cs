using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServiceTeamSlot : MonoBehaviour
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

    private ServiceTeam _serviceTeam;

    public void Init(ServiceTeam serviceTeam)
    {
        _serviceTeam = serviceTeam;
        serviceTeam.onValueChanged += UpdateUI;
        serviceTeam.onProgressValueChanged += UpdateProgressUI;
        _upgradeButton.onClick.AddListener(Upgrade);
        UpdateUI(serviceTeam);
    }

    private void OnEnable()
    {
        if (_serviceTeam != null)
        {
            _serviceTeam.onValueChanged += UpdateUI;
            _serviceTeam.onProgressValueChanged += UpdateProgressUI;
        }
    }

    private void OnDisable()
    {
        _serviceTeam.onValueChanged -= UpdateUI;
        _serviceTeam.onProgressValueChanged -= UpdateProgressUI;
    }

    public void UpdateUI(ServiceTeam serviceTeam)
    {
        if (serviceTeam.Level == 0)
        {
            _unlockedCoverImage.SetActive(true);
        }
        else
        {
            _nameText.text = serviceTeam.Name;
            _outputText.text = $"Output Amount : {serviceTeam.Output.Amount}";
            _leadTimeText.text = $"Lead Time : {serviceTeam.LeadTime}";
            _unlockConditionText.text = $"Unlock Condition : {serviceTeam.UnlockCondition.Amount}";
            _upgradeCostText.text = $"Upgrade Cost : {serviceTeam.UpgradeCost.Amount}";
            _backgroundImage.sprite = serviceTeam.Background;

            if (_unlockedCoverImage.activeInHierarchy) _unlockedCoverImage.SetActive(false);
        }
    }

    public void UpdateProgressUI(float progress)
    {
        _progressFillImage.fillAmount = progress;
    }

    public void Upgrade()
    {
        _serviceTeam.Upgrade();
    }
}
