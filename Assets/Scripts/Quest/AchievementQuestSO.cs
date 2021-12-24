using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementQuest", menuName = "ScriptableObject/Quest/AchievementQuest")]
public class AchievementQuestSO : QuestSO
{
    [SerializeField] private string _formula;

    public int Level { get; private set; }

    public override void ReceiveReward()
    {
        if (Clearable)
        {
            Level += 1;
            Required = int.Parse(new DataTable().Compute(string.Format(_formula, Level), null).ToString());
            onValueChanged?.Invoke(this);
        }
    }
}
