using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Agency", menuName = "ScriptableObject/Department/Agency")]
public class AgencySO : ScriptableObject
{
    [field: SerializeField] public int MaxLevel { get; private set; }
    [field: SerializeField] public Offer[] Offers { get; private set; }
    [field: SerializeField] public int[] MaxExp { get; private set; }

    [SerializeField] private ChiefBundleSO[] _chiefBundles;

    private int _level;
    public int Level
    {
        get { return _level; }

        private set
        {
            _level = value;

            onLevelValueChanged?.Invoke(value);
            onMaxExpValueChanged?.Invoke(MaxExp[value]);
        }
    }    

    private int _exp;
    public int Exp
    {
        get { return _exp; }

        set
        {
            _exp = value;
            if (value >= MaxExp[Level])
            {
                _exp -= MaxExp[Level];
                Level += 1;
            }
            onExpValueChanged?.Invoke(_exp);
        }
    }

    public UnityAction<int> onLevelValueChanged;
    public UnityAction<int> onMaxExpValueChanged;
    public UnityAction<int> onExpValueChanged;

    public Chief[] Suggest(int count)
    {
        // Calculate exp only when level is lower than maxlevel
        if (Level < MaxLevel) Exp += count;

        // Pick
        // 0 : Bronze
        // 1 : Silver
        // 2 : Gold
        // 3 : Platinum
        // 4 : Diamond

        Chief[] chiefs = new Chief[count];
        for (int i = 0; i < count; i++)
        {
            int index = RandomPick();

            chiefs[i] = _chiefBundles[Random.Range(0, _chiefBundles.Length)].Chiefs[index];
            chiefs[i].Amount++;
        }

        return chiefs;
    }

    public int RandomPick()
    {
        float[] percentages = Offers[Level].Percentages;

        float value = Random.Range(0f, 100f);
        for (int i = 0; i < percentages.Length; i++)
        {
            if (value < percentages[i])
                return i;

            value -= percentages[i];
        }

        return -1;
    }
}

[System.Serializable]
public class Offer
{
    [field: SerializeField] public float[] Percentages;
}
