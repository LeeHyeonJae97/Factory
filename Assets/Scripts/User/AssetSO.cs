using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AssetType { Gold, Diamond, ResourceA, ResourceB, ResourceC}

[CreateAssetMenu(fileName = "Asset", menuName = "ScriptableObject/Asset")]
public class AssetSO : ScriptableObject
{
    [field: SerializeField] public AssetType Type { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public int Amount { get; private set; }

    public UnityAction<AssetType, int> onValueChanged;

    public void Init(int amount)
    {
        Amount = amount;
        onValueChanged?.Invoke(Type, Amount);
    }

    public bool Affordable(int amount)
    {
        return Amount >= amount;
    }

    public void Gain(int amount)
    {
        Amount += amount;
        onValueChanged?.Invoke(Type, Amount);
    }

    public void Lose(int amount)
    {
        Amount -= amount;
        onValueChanged?.Invoke(Type, Amount);
    }
}
