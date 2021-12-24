using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;
using UnityEngine.Events;

public enum StatType { AbilityPoint }

public class StatPoint
{
    private static StatPoint[] _instances;

    public static int Length => _instances.Length;

    public StatType Type { get; private set; }

    public int Amount { get; private set; }

    public UnityAction<StatPoint> onValueChanged;

    public static void Load(EasyFileSave file)
    {
        _instances = new StatPoint[System.Enum.GetValues(typeof(StatType)).Length];

        for (int i = 0; i < _instances.Length; i++)
        {
            _instances[i] = new StatPoint();
            _instances[i].Type = (StatType)i;
            _instances[i].Amount = file.GetInt($"{(StatType)i}Amount");
            _instances[i].onValueChanged?.Invoke(_instances[i]);
        }
    }

    public static void Save(EasyFileSave file)
    {
        for (int i = 0; i < _instances.Length; i++)
            file.Add($"{_instances[i].Type}Amount", _instances[i].Amount);
    }

    public static StatPoint Get(int index)
    {
        return _instances[index];
    }

    public static StatPoint Get(StatType type)
    {
        return _instances[(int)type];
    }

    public void Gain()
    {
        Amount += 1;
        onValueChanged?.Invoke(this);
    }
}
