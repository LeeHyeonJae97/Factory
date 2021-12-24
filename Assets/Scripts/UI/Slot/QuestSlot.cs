using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour, IPresenter<QuestSO>, IObserver
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private TextMeshProUGUI _rewardText;
    [SerializeField] private Button _receiveRewardButton;

    private QuestSO _quest;

    private void Start()
    {
        _receiveRewardButton.onClick.AddListener(() => _quest.ReceiveReward());
    }

    private void OnEnable()
    {
        Observe(true);
    }

    private void OnDisable()
    {
        Observe(false);
    }

    public void SetInfo(QuestSO quest)
    {
        _quest = quest;
        UpdateUI(quest);
    }

    public void Observe(bool value)
    {
        if (_quest == null) return;

        if (value)
            _quest.onValueChanged += UpdateUI;
        else
            _quest.onValueChanged -= UpdateUI;
    }

    private void UpdateUI(QuestSO quest)
    {
        _titleText.text = _quest.Title;
        _countText.text = $"{quest.Current} / {quest.Required}";
        _rewardText.text = $"{quest.Reward}";
    }
}
