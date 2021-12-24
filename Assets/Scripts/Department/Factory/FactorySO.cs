using Extension;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TigerForge;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Factory", menuName = "ScriptableObject/Factory")]
public class FactorySO : ScriptableObject
{
    [SerializeField] private int _id;
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Background { get; private set; }
    [SerializeField] private float _leadTime;
    [SerializeField] private string[] _inputAmountFormulas;
    [SerializeField] private string _outputAmountFormula;
    [SerializeField] private string _upgradeCostFormula;
    [field: SerializeField] public int UnlockConditionAmount { get; private set; }

    public int Level { get; private set; }
    public float LeadTime { get; private set; }
    public int[] InputAmounts { get; private set; }
    public int OutputAmount { get; private set; }
    public int UpgradeCost { get; private set; }
    public float Progress { get; private set; }

    private ResourceSO[] _inputs;
    private ProductSO _output;
    private MoneySO _upgradeOffering;
    private CapabilitySO _fame;
    private ChiefBundleSO _chiefBundle;

    public UnityAction<FactorySO> onValueChanged;
    public UnityAction<float> onProgressValueChanged;

    public void Load(EasyFileSave file, ResourceSO[] inputs, ProductSO output, MoneySO upgradeOffering, CapabilitySO fame, ChiefBundleSO chiefBundle)
    {
        if (_inputAmountFormulas.Length != inputs.Length)
        {
            Debug.LogError("Error");
            return;
        }

        InputAmounts = new int[_inputAmountFormulas.Length];

        Level = file.GetInt($"{_id}Level");
        Progress = file.GetFloat($"{_id}Progress");
        _inputs = inputs;
        _output = output;
        _upgradeOffering = upgradeOffering;
        _fame = fame;
        _chiefBundle = chiefBundle;

        UpdateValue();

        for (int i = 0; i < _chiefBundle.Chiefs.Length; i++)
            _chiefBundle.Chiefs[i].onAmountValueChanged += (amount) => UpdateValue();

        AbilitySO.Get(AbilityType.LeadTime).onValueChanged += (ability) => UpdateValue();
        AbilitySO.Get(AbilityType.Output).onValueChanged += (ability) => UpdateValue();
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
        for (int i = 0; i < InputAmounts.Length; i++)
            InputAmounts[i] = int.Parse(new DataTable().Compute(string.Format(_inputAmountFormulas[i], Level), null).ToString());
        OutputAmount = (int)(int.Parse(new DataTable().Compute(string.Format(_outputAmountFormula, Level), null).ToString()) * (1 + (_chiefBundle.Buff + AbilitySO.Get(AbilityType.Output).Buff)));
        UpgradeCost = int.Parse(new DataTable().Compute(string.Format(_upgradeCostFormula, Level), null).ToString());

        onValueChanged?.Invoke(this);
    }

    public IEnumerator ProcessCoroutine()
    {
        float progress = Time.deltaTime / LeadTime;

        while (true)
        {
            if (Progress >= 1 && Producable())
            {
                Progress = 0;
                Produce();
            }
            else
            {
                Progress += progress;
            }
            onProgressValueChanged?.Invoke(Progress);
            yield return null;
        }
    }

    private bool Producable()
    {
        // Check all inputs are affordable
        for (int i = 0; i < _inputs.Length; i++)
        {
            if (!_inputs[i].Affordable(InputAmounts[i]))
                return false;
        }

        return true;
    }

    private void Produce()
    {
        // Spend input
        for (int i = 0; i < _inputs.Length; i++)
            _inputs[i].Lose(InputAmounts[i]);

        // Earn output
        _output.Gain(OutputAmount);
    }
}
