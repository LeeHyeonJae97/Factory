using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Product", menuName = "ScriptableObject/Product")]
public class ProductSO : ScriptableObject
{
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }     
    [field: SerializeField] public int Price { get; private set; }
    public int Amount { get; private set; }

    public UnityAction<int, int> onValueChanged;

    public void Init(int amount)
    {
        Amount = amount;
        onValueChanged?.Invoke(Id, Amount);
    }

    public bool Affordable(int amount)
    {
        return Amount >= amount;
    }

    public void Gain(int amount)
    {
        Amount += amount;
        onValueChanged?.Invoke(Id, Amount);
    }

    public void Lose(int amount)
    {
        Amount -= amount;
        onValueChanged?.Invoke(Id, Amount);
    }
}
