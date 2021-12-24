using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestTable", menuName = "ScriptableObject/Quest/QuestTable")]
public class QuestTableSO : ScriptableObject
{
    [field: SerializeField] public List<DailyQuestSO> DailyQuests { get; private set; }
    [field: SerializeField] public List<AchievementQuestSO> AchievementQuests { get; private set; }

    private Dictionary<string, DailyQuestSO> dailyQuestDic;
    private Dictionary<string, AchievementQuestSO> achievementQuestDic;

    public void Load()
    {
        // ���̺�� ���� �ε�

        // NOTE :
        // Daily�� cleared�� true��� ���� ��¥�� ����� clearedDate�� ���ؼ� Ŭ���� �������� ����
    }

    public void Save()
    {
        // Quest�� Current, Cleared, Executable ����
    }

    public void Perform(string id)
    {
        if (dailyQuestDic.TryGetValue(id, out DailyQuestSO dailyQuest)) dailyQuest.Perform();
        if (achievementQuestDic.TryGetValue(id, out AchievementQuestSO achievementQuest)) achievementQuest.Perform();
    }
}
