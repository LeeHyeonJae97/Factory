using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestUI : MonoBehaviour, ITab
{
    [SerializeField] private QuestSlot _slotPrefab;
    [SerializeField] private Transform _slotHolder;
    [SerializeField] private QuestTableSO _questTable;

    private List<QuestSlot> _slots = new List<QuestSlot>();

    private void Awake()
    {
        // get max count of farms in bundle
        int max = Mathf.Max(_questTable.DailyQuests.Count, _questTable.AchievementQuests.Count);

        for (int i = 0; i < max; i++)
            _slots.Add(Instantiate(_slotPrefab, _slotHolder));
    }

    public void SelectTab(int index)
    {
        // set index farm bundle info
        List<QuestSO> items;
        switch (index)
        {
            case 0:
                items = new List<QuestSO>(_questTable.DailyQuests.Cast<QuestSO>());
                break;

            case 1:
                items = new List<QuestSO>(_questTable.AchievementQuests.Cast<QuestSO>());
                break;

            default:
                Debug.LogError("Error (QuestUI.SelectTab) : wrong index");
                return;
        }

        for (int i = 0; i < _slots.Count; i++)
        {
            if (i < items.Count)
            {
                _slots[i].SetInfo(items[i]);
                _slots[i].gameObject.SetActive(true);
            }
            else
            {
                _slots[i].gameObject.SetActive(false);
            }
        }
    }
}
