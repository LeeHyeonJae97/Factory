using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Chief", menuName = "ScriptableObject/Chief")]
public class ChiefSO : ScriptableObject
{
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public float Buff { get; private set; }
    [field: SerializeField] public Sprite Preview { get; private set; }

    private int _amount;
    public int Amount
    {
        get { return _amount; }

        set
        {
            _amount = value;
            onAmountValueChanged?.Invoke(value);
        }
    }

    public UnityAction<int> onAmountValueChanged;

    public void Load(EasyFileSave file)
    {
        Amount = file.GetInt($"{Id}Amount");
    }

    public void Save(EasyFileSave file)
    {
        file.Add($"{Id}Amount", _amount);
    }
}
