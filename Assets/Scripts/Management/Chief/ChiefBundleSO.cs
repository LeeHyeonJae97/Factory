using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ChiefBundle", menuName = "ScriptableObject/ChiefBundle")]
public class ChiefBundleSO : ScriptableObject
{
    [field: SerializeField] public Chief[] Chiefs { get; private set; }

    public float BuffValue
    {
        get
        {
            float value = 0;
            for (int i = 0; i < Chiefs.Length; i++)
                value += Chiefs[i].Buff * Chiefs[i].Amount;

            return value;
        }
    }
}

[System.Serializable]
public class Chief
{
    [field: SerializeField] public Sprite Preview { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Department { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public float Buff { get; private set; }

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
}
