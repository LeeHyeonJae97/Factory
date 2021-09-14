using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Fame", menuName = "ScriptableObject/Fame")]
public class FameSO : ScriptableObject
{
    [SerializeField] private string _maxExpFormula;
    private int _maxExp;

    private int _level;
    public int Level
    {
        get { return _level; }

        private set
        {
            _level = value;
            _maxExp = int.Parse(new DataTable().Compute(string.Format(_maxExpFormula, value), null).ToString());
            onLevelValueChanged?.Invoke(value);
        }
    }

    private int _exp;
    public int Exp
    {
        get { return _exp; }

        set
        {
            _exp = value;
            if (value >= _maxExp)
            {
                _exp -= _maxExp;
                Level += 1;
            }
            onExpRatioValueChanged?.Invoke((float)_exp / _maxExp);
        }
    }

    public UnityAction<int> onLevelValueChanged;
    public UnityAction<float> onExpRatioValueChanged;

    public void Init(int level, int exp)
    {
        Level = level;
        Exp = exp;
        _maxExp = int.Parse(new DataTable().Compute(string.Format(_maxExpFormula, level), null).ToString());
    }
}
