using System.Collections;
using System.Collections.Generic;
using System.Data;
using TigerForge;
using UnityEngine;
using UnityEngine.Events;

// Output, LeadTime, MaxTask
public enum AbilityType { LeadTime, Output }

[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObject/Ability")]
public class AbilitySO : ScriptableObject
{
    private static AbilitySO[] _instances;

    public static int Length => _instances.Length;

    [field: SerializeField] public AbilityType Type { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [SerializeField] private string _buffFormula;
    [SerializeField] private MoneySO _upgradeOffering;
    [SerializeField] private string _upgradeCostFormula;
    [field: SerializeField] public Sprite Icon { get; private set; }

    public int Level { get; private set; }
    public float Buff { get; private set; }
    public float NextLevelBuff { get; private set; }
    public int UpgradeCost { get; private set; }

    public UnityAction<AbilitySO> onValueChanged;

    public static void Load(EasyFileSave file)
    {
        if (_instances == null)
        {
            var tmp = Resources.LoadAll<AbilitySO>("Ability");
            _instances = new AbilitySO[tmp.Length];

            for (int i = 0; i < tmp.Length; i++)
                _instances[(int)tmp[i].Type] = tmp[i];
        }

        for (int i = 0; i < _instances.Length; i++)
        {
            // load data
        }
    }

    public static void Save(EasyFileSave file)
    {

        for (int i = 0; i < _instances.Length; i++)
        {
            // load data
        }
    }

    public static AbilitySO Get(int index)
    {
        return _instances[index];
    }

    public static AbilitySO Get(AbilityType type)
    {
        return _instances[(int)type];
    }

    public void Upgrade()
    {
        if (_upgradeOffering.Affordable(UpgradeCost))
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
        Buff = int.Parse(new DataTable().Compute(string.Format(_buffFormula, Level), null).ToString()) * 0.01f;
        NextLevelBuff = int.Parse(new DataTable().Compute(string.Format(_buffFormula, Level + 1), null).ToString()) * 0.01f;
        UpgradeCost = int.Parse(new DataTable().Compute(string.Format(_upgradeCostFormula, Level), null).ToString());

        onValueChanged?.Invoke(this);
    }
}
