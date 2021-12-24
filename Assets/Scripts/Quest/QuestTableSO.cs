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
        // 세이브된 정보 로드

        // NOTE :
        // Daily는 cleared가 true라면 오늘 날짜와 저장된 clearedDate를 비교해서 클리어 가능한지 결정
    }

    public void Save()
    {
        // Quest의 Current, Cleared, Executable 저장
    }

    public void Perform(string id)
    {
        if (dailyQuestDic.TryGetValue(id, out DailyQuestSO dailyQuest)) dailyQuest.Perform();
        if (achievementQuestDic.TryGetValue(id, out AchievementQuestSO achievementQuest)) achievementQuest.Perform();
    }
}
