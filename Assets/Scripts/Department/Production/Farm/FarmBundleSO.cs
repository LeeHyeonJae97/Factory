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
    [field: SerializeField] public IncreasingBill<AssetSO> Output { get; private set; }
    [field: SerializeField] public float LeadTime { get; private set; }
    [field: SerializeField] public Bill<FameSO> UnlockCondition { get; private set; }
    [field: SerializeField] public IncreasingBill<AssetSO> UpgradeCost { get; private set; }
    [field: SerializeField] public Sprite Background { get; private set; }
    public int Level { get; private set; }

    private float _progress;

    public UnityAction<Farm> onValueChanged;
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
        Output.SetAmount(Level);
        UpgradeCost.SetAmount(Level);

        onValueChanged?.Invoke(this);
    }

    public IEnumerator ProcessCoroutine()
    {
        float progress = Time.deltaTime / LeadTime;

        while (true)
        {
            if (_progress >= 1)
            {
                _progress = 0;
                Output.Asset.Gain(Output.Amount);
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
