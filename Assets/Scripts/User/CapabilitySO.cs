using System.Collections;
using System.Collections.Generic;
using System.Data;
using TigerForge;
using UnityEngine;
using UnityEngine.Events;

public enum CapabilityType { Exp, Fame }

[CreateAssetMenu(fileName = "Capability", menuName = "ScriptableObject/Capability")]
public class CapabilitySO : ScriptableObject
{
    private static CapabilitySO[] _instances;

    public static int Length => _instances.Length;

    [field: SerializeField] public CapabilityType Type { get; private set; }
    [field: SerializeField] public string Name { get; private set; }

    [SerializeField] private string _maxExpFormula;
    public int MaxExp { get; private set; }

    private int _level;
    public int Level
    {
        get { return _level; }

        private set
        {
            _level = value;
            MaxExp = int.Parse(new DataTable().Compute(string.Format(_maxExpFormula, value), null).ToString());
            onValueChanged?.Invoke(this);
        }
    }

    private int _exp;
    public int Exp
    {
        get { return _exp; }

        set
        {
            _exp = value;
            if (value >= MaxExp)
            {
                _exp -= MaxExp;
                Level += 1;
            }
            onValueChanged?.Invoke(this);
        }
    }

    public UnityAction<CapabilitySO> onValueChanged;

    public static void Load(EasyFileSave file)
    {
        if (_instances == null)
        {
            var tmp = Resources.LoadAll<CapabilitySO>("Capability");
            _instances = new CapabilitySO[tmp.Length];

            for (int i = 0; i < tmp.Length; i++)
                _instances[(int)tmp[i].Type] = tmp[i];
        }

        for (int i = 0; i < _instances.Length; i++)
        {
            _instances[i].Level = file.GetInt($"{_instances[i].Type}Level");
            _instances[i].Exp = file.GetInt($"{_instances[i].Type}Exp");
        }
    }

    public static void Save(EasyFileSave file)
    {
        for (int i = 0; i < _instances.Length; i++)
        {
            file.Add($"{_instances[i].Type}Level", _instances[i].Level);
            file.Add($"{_instances[i].Type}Exp", _instances[i].Exp);
        }
    }

    public static CapabilitySO Get(int index)
    {
        return _instances[index];
    }

    public static CapabilitySO Get(CapabilityType type)
    {
        return _instances[(int)type];
    }
}
