using Extension;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TigerForge;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SalesTeam", menuName = "ScriptableObject/SalesTeam")]
public class SalesTeamSO : ScriptableObject
{
    [SerializeField] private int _id;
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Background { get; private set; }
    [SerializeField] private float _leadTime;
    [SerializeField] private string _inputAmountFormula;
    [SerializeField] private string _upgradeCostFormula;
    [field: SerializeField] public int UnlockConditionAmount { get; private set; }

    public int Level { get; private set; }
    public float LeadTime { get; private set; }
    public int InputAmount { get; private set; }
    public int UpgradeCost { get; private set; }
    public ProductSO Input { get; private set; }
    public float Progress { get; private set; }

    private MoneySO _output;
    private MoneySO _upgradeOffering;
    private CapabilitySO _fame;
    private ChiefBundleSO _chiefBundle;

    public UnityAction<SalesTeamSO> onValueChanged;
    public UnityAction<float> onProgressValueChanged;

    public void Load(EasyFileSave file, ProductSO input, MoneySO output, MoneySO upgradeOffering, CapabilitySO fame, ChiefBundleSO chiefBundle)
    {
        Level = file.GetInt($"{_id}Level");
        Progress = file.GetFloat($"{_id}Progress");

        Input = input;
        _output = output;
        _upgradeOffering = upgradeOffering;
        _fame = fame;
        _chiefBundle = chiefBundle;

        UpdateValue();

        for (int i = 0; i < _chiefBundle.Chiefs.Length; i++)
            _chiefBundle.Chiefs[i].onAmountValueChanged += (amount) => UpdateValue();

        AbilitySO.Get(AbilityType.LeadTime).onValueChanged += (ability) => UpdateValue();  
    }

    public void Save(EasyFileSave file)
    {
        file.Add($"{_id}Level", Level);
        file.Add($"{_id}Progress", Progress);
    }

    public void Upgrade()
    {
        // Unlock / LevelUp
        if ((Level == 0 && _fame.Level >= UnlockConditionAmount && _upgradeOffering.Affordable(UpgradeCost)) ||
            (Level >= 1 && _upgradeOffering.Affordable(UpgradeCost)))
        {
            _upgradeOffering.Lose(UpgradeCost);

            Level += 1;
            UpdateValue();
        }
        else
        {
            Debug.Log("Cannot upgrade");
        }
    }

    private void UpdateValue()
    {
        LeadTime = (int)(_leadTime * (1 - AbilitySO.Get(AbilityType.LeadTime).Buff));
        InputAmount = int.Parse(new DataTable().Compute(string.Format(_inputAmountFormula, Level), null).ToString());
        UpgradeCost = int.Parse(new DataTable().Compute(string.Format(_upgradeCostFormula, Level), null).ToString());

        onValueChanged?.Invoke(this);
    }

    public IEnumerator ProcessCoroutine()
    {
        float progress = Time.deltaTime / LeadTime;

        while (true)
        {
            if (Progress >= 1 && Sellable())
            {
                Progress = 0;
                Sell();
            }
            else
            {
                Progress += progress;
            }
            onProgressValueChanged?.Invoke(Progress);
            yield return null;
        }
    }

    private bool Sellable()
    {
        // have enough stock
        return Input.Amount > 0;
    }

    private void Sell()
    {
        // Spend product
        int amount = Mathf.Min(Input.Amount, InputAmount);
        Input.Lose(amount);

        // Earn money
        _output.Gain(Input.Price * amount);
    }
}
