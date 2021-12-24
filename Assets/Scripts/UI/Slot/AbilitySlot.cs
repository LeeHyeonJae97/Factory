using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Extension;

public class AbilitySlot : MonoBehaviour, IObserver
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _buffText;
    [SerializeField] private TextMeshProUGUI _upgradeCostText;
    [SerializeField] private Button _upgradeButton;

    [SerializeField] private AbilityType _abilityType;

    private void Start()
    {
        AbilitySO ability = AbilitySO.Get(_abilityType);
        UpdateUI(ability);

        _upgradeButton.onClick.AddListener(() => ability.Upgrade());
    }

    private void OnEnable()
    {
        Observe(true);
    }

    private void OnDisable()
    {
        Observe(false);
    }

    public void Observe(bool value)
    {
        AbilitySO ability = AbilitySO.Get(_abilityType);

        if (ability == null) return;

        if (value)
            ability.onValueChanged += UpdateUI;
        else
            ability.onValueChanged -= UpdateUI;
    }

    private void UpdateUI(AbilitySO ability)
    {
        _iconImage.sprite = ability.Icon;
        _nameText.text = $"{ability.Name}";
        _levelText.text = $"{ability.Level}";
        _buffText.text = $"{ability.Buff} -> {ability.NextLevelBuff}";
        _upgradeCostText.text = $"{ability.UpgradeCost}";
    }
}
