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

    private void Start()
    {
        _upgradeButton.onClick.AddListener(() => _serviceTeam.Upgrade());
    }

    public void SetInfo(ServiceTeam serviceTeam)
    {
        Observe(false);
        _serviceTeam = serviceTeam;
        Observe(true);

        UpdateUI(serviceTeam);
    }

    private void OnEnable()
    {
        Observe(true);
    }

    private void OnDisable()
    {
        Observe(false);
    }

    private void Observe(bool value)
    {
        if (_serviceTeam == null) return; 

        if(value)
        {
            _serviceTeam.onValueChanged += UpdateUI;
            _serviceTeam.onProgressValueChanged += UpdateProgressUI;
        }
        else
        {
            _serviceTeam.onValueChanged -= UpdateUI;
            _serviceTeam.onProgressValueChanged -= UpdateProgressUI;
        }
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
}
