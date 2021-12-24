using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessengerUI : MonoBehaviour
{
    //[SerializeField] private Conversation[] _conversations;
    //[SerializeField] private SentMessageSlot[] _slots;

    //[SerializeField] private VerticalLayoutGroup[] _groups;

    //private void Start()
    //{
    //    _groups = GetComponentsInChildren<VerticalLayoutGroup>(true);
    //    Init(_conversations[0].messages);
    //}

    //public void Init(string[] data)
    //{
    //    for (int i = 0; i < _slots.Length; i++)
    //    {
    //        if (i < data.Length)
    //        {
    //            _slots[i].SetInfo(data[i]);
    //            _slots[i].gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            _slots[i].gameObject.SetActive(false);
    //        }
    //    }

    //    for (int i = _groups.Length - 1; i >= 0; i--)
    //    {
    //        if (_groups[i].enabled)
    //            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_groups[i].transform);
    //    }
    //}

    //public void SelectTab(int index)
    //{
    //    Init(_conversations[index].messages);
    //}
}
