using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;

public class SalesTeamUI : MonoBehaviour
{
    [Header("Slot")]
    [SerializeField] protected SalesTeamSlot _slotPrefab;
    [SerializeField] protected Transform _slotHolder;
    [SerializeField] protected SalesTeamBundleSO[] _salesTeamBundles; // have to change load asynchronously        

    [Header("Chief")]
    [SerializeField] protected ChiefSlot[] _chiefSlots;
    [SerializeField] protected ChiefBundleSO _chiefBundle;

    protected List<SalesTeamSlot> _slots = new List<SalesTeamSlot>();

    private void Awake()
    {
        // get max count of farms in bundle
        int max = 0;
        if (_salesTeamBundles.Length > 1)
        {
            for (int i = 0; i < _salesTeamBundles.Length; i++)
            {
                int length = _salesTeamBundles[i].SalesTeams.Length;
                if (max < length) max = length;
            }
        }
        else
        {
            max = _salesTeamBundles[0].SalesTeams.Length;
        }

        // instantiate max count of farm slots
        for (int i = 0; i < max; i++)
            _slots.Add(Instantiate(_slotPrefab, _slotHolder));

        //** NOTE : Maybe error
        // set chief info
        ChiefSO[] chiefs = _chiefBundle.Chiefs;
        for (int i = 0; i < _chiefSlots.Length; i++)
            _chiefSlots[i].SetInfo(chiefs[i]);
    }

    public void SelectTab(int index)
    {
        // to invoke OnDisable
        for (int i = 0; i < _slots.Count; i++)
            _slots[i].gameObject.SetActive(false);

        // set farm info
        SalesTeamSO[] items = _salesTeamBundles[index].SalesTeams;
        for (int i = 0; i < items.Length; i++)
        {
            _slots[i].SetInfo(items[i]);
            _slots[i].gameObject.SetActive(true);
        }
    }
}
