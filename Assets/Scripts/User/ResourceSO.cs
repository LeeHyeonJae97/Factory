using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;
using UnityEngine.Events;

public enum ResourceType { ResourceA, ResourceB, ResourceC }

[CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObject/Resource")]
public class ResourceSO : ScriptableObject
{
    private static ResourceSO[] _instances;

    public static int Length => _instances.Length;

    [field: SerializeField] public ResourceType Type { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    public int Amount { get; private set; }

    public UnityAction<ResourceSO> onValueChanged;

    public static void Load(EasyFileSave file)
    {
        if (_instances == null)
        {
            var tmp = Resources.LoadAll<ResourceSO>("Resource");
            _instances = new ResourceSO[tmp.Length];

            for (int i = 0; i < tmp.Length; i++)
                _instances[(int)tmp[i].Type] = tmp[i];
        }

        for (int i = 0; i < _instances.Length; i++)
        {
            _instances[i].Amount = file.GetInt($"{_instances[i].Type}Amount");
            _instances[i].onValueChanged?.Invoke(_instances[i]);
        }
    }

    public static void Save(EasyFileSave file)
    {
        for (int i = 0; i < _instances.Length; i++)
            file.Add($"{_instances[i].Type}Amount", _instances[i].Amount);
    }

    public static ResourceSO Get(int index)
    {
        return _instances[index];
    }

    public static ResourceSO Get(ResourceType type)
    {
        return _instances[(int)type];
    }

    public bool Affordable(int amount)
    {
        return Amount >= amount;
    }

    public void Gain(int amount)
    {
        Amount += amount;
        onValueChanged?.Invoke(this);
    }

    public void Lose(int amount)
    {
        Amount -= amount;
        onValueChanged?.Invoke(this);
    }
}
