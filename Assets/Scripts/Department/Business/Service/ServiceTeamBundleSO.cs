using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ServiceTeamBundle", menuName = "ScriptableObject/ServiceTeamBundle")]
public class ServiceTeamBundleSO : ScriptableObject
{
    [field: SerializeField] public ServiceTeam[] ServiceTeams { get; private set; }

    public void Init()
    {
        // Load data using EasyFileSave

        for (int i = 0; i < ServiceTeams.Length; i++)
            ServiceTeams[i].Init(0);
    }
}

[System.Serializable]
public class ServiceTeam
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public IncreasingBill<FameSO> Output { get; private set; }
    [field: SerializeField] public float LeadTime { get; private set; }
    [field: SerializeField] public Bill<FameSO> UnlockCondition { get; private set; }
    [field: SerializeField] public IncreasingBill<AssetSO> UpgradeCost { get; private set; }
    [field: SerializeField] public Sprite Background { get; private set; }
    public int Level { get; private set; }

    private float _progress;

    public UnityAction<ServiceTeam> onValueChanged;
    public UnityAction<float> onProgressValueChanged;

    public void Init(int level)
    {
        // Data to save/load
        // 1. level
        // 2. progress

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
                Output.Asset.Exp += Output.Amount;
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
