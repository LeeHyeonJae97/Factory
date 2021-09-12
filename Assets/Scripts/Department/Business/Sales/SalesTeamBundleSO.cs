using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SalesTeamBundle", menuName = "ScriptableObject/SalesTeamBundle")]
public class SalesTeamBundleSO : ScriptableObject
{
    [field: SerializeField] public SalesTeam[] SalesTeams { get; private set; }

    public void Init()
    {
        // Load data using EasyFileSave

        for (int i = 0; i < SalesTeams.Length; i++)
            SalesTeams[i].Init(0);
    }
}

[System.Serializable]
public class SalesTeam
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public IncreasingBill<ProductSO> Input { get; private set; }
    [field: SerializeField] public float LeadTime { get; private set; }
    [field: SerializeField] public Bill<FameSO> UnlockCondition { get; private set; }
    [field: SerializeField] public IncreasingBill<AssetSO> UpgradeCost { get; private set; }
    [field: SerializeField] public Sprite Background { get; private set; }
    [field: SerializeField] public AssetSO Gold { get; private set; }
    public int Level { get; private set; }

    private float _progress;

    public UnityAction<SalesTeam> onValueChanged;
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
        Input.SetAmount(Level);
        UpgradeCost.SetAmount(Level);
        onValueChanged?.Invoke(this);
    }

    public IEnumerator ProcessCoroutine()
    {
        float progress = Time.deltaTime / LeadTime;

        while (true)
        {
            if (_progress >= 1 && Sellable())
            {
                _progress = 0;
                Sell();
            }
            else
            {
                _progress += progress;
            }
            onProgressValueChanged?.Invoke(_progress);
            yield return null;
        }
    }

    private bool Sellable()
    {
        // have enough stock
        return Input.Asset.Amount > 0;
    }

    private void Sell()
    {
        // Spend product
        int amount = Mathf.Min(Input.Asset.Amount, Input.Amount);
        Input.Asset.Lose(amount);

        // Earn money
        Gold.Gain(Input.Asset.Price * amount);
    }
}
