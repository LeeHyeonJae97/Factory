using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FactoryBundle", menuName = "ScriptableObject/FactoryBundle")]
public class FactoryBundleSO : ScriptableObject
{
    [field: SerializeField] public Factory[] Factories { get; private set; }

    public void Init()
    {
        // Load data using EasyFileSave

        for (int i = 0; i < Factories.Length; i++)
            Factories[i].Init(0);
    }
}

[System.Serializable]
public class Factory
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public IncreasingBill<AssetSO>[] Inputs { get; private set; }
    [field: SerializeField] public IncreasingBill<ProductSO> Output { get; private set; }
    [field: SerializeField] public float LeadTime { get; private set; }
    [field: SerializeField] public Bill<FameSO> UnlockCondition { get; private set; }
    [field: SerializeField] public IncreasingBill<AssetSO> UpgradeCost { get; private set; }
    [field: SerializeField] public Sprite Background { get; private set; }
    public int Level { get; private set; }

    private float _progress;

    public UnityAction<Factory> onValueChanged;
    public UnityAction<float> onProgressValueChanged;

    public void Init(int level)
    {
        Level = level;
        UpdateValue();
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
        for (int i = 0; i < Inputs.Length; i++)
            Inputs[i].SetAmount(Level);
        Output.SetAmount(Level);
        UpgradeCost.SetAmount(Level);

        onValueChanged?.Invoke(this);
    }

    public IEnumerator ProcessCoroutine()
    {
        float progress = Time.deltaTime / LeadTime;

        while (true)
        {
            if (_progress >= 1 && Producable())
            {
                _progress = 0;
                Produce();
            }
            else
            {
                _progress += progress;
            }
            onProgressValueChanged?.Invoke(_progress);
            yield return null;
        }
    }

    private bool Producable()
    {
        // Check all inputs are affordable
        for (int i = 0; i < Inputs.Length; i++)
        {
            if (!Inputs[i].Asset.Affordable(Inputs[i].Amount))
                return false;
        }

        return true;
    }

    private void Produce()
    {
        // Spend input
        for (int i = 0; i < Inputs.Length; i++)
            Inputs[i].Asset.Lose(Inputs[i].Amount);

        // Earn output
        Output.Asset.Gain(Output.Amount);
    }
}
