using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FarmBundle", menuName = "ScriptableObject/FarmBundle")]
public class FarmBundleSO : ScriptableObject
{    
    [field: SerializeField] public Farm[] Farms { get; private set; }

    public void Init()
    {
        // Load data using EasyFileSave

        for (int i = 0; i < Farms.Length; i++)
            Farms[i].Init(0);
    }
}

[System.Serializable]
public class Farm
{
    [field: SerializeField] public string Name { get; private set; }    
    [field: SerializeField] public Bill<FameSO> UnlockCondition { get; private set; }
    [field: SerializeField] public IncreasingBill<AssetSO> UpgradeCost { get; private set; }
    [field: SerializeField] public Sprite Background { get; private set; }
    [SerializeField] private IncreasingBill<AssetSO> _output;
    [SerializeField] private float _leadTime;
    [SerializeField] private ChiefBundleSO _chiefBundle;

    public int Level { get; private set; }
    public int Output => (int)(_output.Amount * (1 * _chiefBundle.BuffValue));
    public float LeadTime => _leadTime;

    private float _progress;

    public UnityAction<Farm> onValueChanged;
    public UnityAction<float> onProgressValueChanged;

    public void Init(int level)
    {
        // Data to save/load
        // 1. level
        // 2. progress

        Level = level;
        UpdateValue();

        for (int i = 0; i < _chiefBundle.Chiefs.Length; i++)
            _chiefBundle.Chiefs[i].onAmountValueChanged += (amount) => onValueChanged?.Invoke(this);
    }

    public void Upgrade()
    {
        // Unlock / LevelUp
        if ((Level == 0 && UnlockCondition.Asset.Level >= UnlockCondition.Amount && UpgradeCost.Asset.Affordable(UpgradeCost.Amount)) ||
            (Level >= 1 && UpgradeCost.Asset.Affordable(UpgradeCost.Amount)))
        {
            Level += 1;
            UpgradeCost.Asset.Lose(UpgradeCost.Amount);
            UpdateValue();
        }
        else
        {
            Debug.Log("Cannot upgrade");
        }
    }

    private void UpdateValue()
    {
        _output.SetAmount(Level);
        UpgradeCost.SetAmount(Level);

        onValueChanged?.Invoke(this);
    }

    public IEnumerator ProcessCoroutine()
    {
        float progress = Time.deltaTime / _leadTime;

        while (true)
        {
            if (_progress >= 1)
            {
                _progress = 0;
                _output.Asset.Gain(Output);
            }
            else
            {
                _progress += progress;
            }
            onProgressValueChanged?.Invoke(_progress);
            yield return null;
        }
    }
}
