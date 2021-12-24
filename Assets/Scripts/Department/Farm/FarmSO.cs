using Extension;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TigerForge;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Farm", menuName = "ScriptableObject/Farm")]
public class FarmSO : ScriptableObject
{
    [SerializeField] private int _id;
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Background { get; private set; }
    [SerializeField] private float _leadTime;
    [SerializeField] private string _outputAmountFormula;
    [SerializeField] private string _upgradeCostFormula;
    [field: SerializeField] public int UnlockConditionAmount { get; private set; }

    public int Level { get; private set; }
    public float LeadTime { get; private set; }
    public int OutputAmount { get; private set; }
    public int UpgradeCost { get; private set; }
    public float Progress { get; private set; }

    private ResourceSO _output;
    private MoneySO _upgradeOffering;
    private CapabilitySO _fame;
    private ChiefBundleSO _chiefBundle;

    public UnityAction<FarmSO> onValueChanged;
    public UnityAction<float> onProgressValueChanged;

    public void Load(EasyFileSave file, ResourceSO output, MoneySO upgradeOffering, CapabilitySO fame, ChiefBundleSO chiefBundle)
    {
        Level = file.GetInt($"{_id}Level");
        Progress = file.GetFloat($"{_id}Progress");
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
        OutputAmount = (int)(int.Parse(new DataTable().Compute(string.Format(_outputAmountFormula, Level), null).ToString()) * (1 + (_chiefBundle.Buff + AbilitySO.Get(AbilityType.Output).Buff)));
        UpgradeCost = int.Parse(new DataTable().Compute(string.Format(_upgradeCostFormula, Level), null).ToString());

        onValueChanged?.Invoke(this);
    }

    public IEnumerator ProcessCoroutine()
    {
        float progress = Time.deltaTime / LeadTime;

        while (true)
        {
            if (Progress >= 1)
            {
                Progress = 0;
                _output.Gain(OutputAmount);
            }
            else
            {
                Progress += progress;
            }
            onProgressValueChanged?.Invoke(Progress);
            yield return null;
        }
    }
}
