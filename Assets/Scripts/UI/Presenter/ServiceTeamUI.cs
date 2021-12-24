using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;

public class ServiceTeamUI : MonoBehaviour
{
    [Header("Slot")]
    [SerializeField] protected ServiceTeamSlot _slotPrefab;
    [SerializeField] protected Transform _slotHolder;
    [SerializeField] protected ServiceTeamBundleSO[] _serviceTeamBundles; // have to change load asynchronously        

    [Header("Chief")]
    [SerializeField] protected ChiefSlot[] _chiefSlots;
    [SerializeField] protected ChiefBundleSO _chiefBundle;

    protected List<ServiceTeamSlot> _slots = new List<ServiceTeamSlot>();

    private void Awake()
    {
        // get max count of farms in bundle
        int max = 0;
        if (_serviceTeamBundles.Length > 1)
        {
            for (int i = 0; i < _serviceTeamBundles.Length; i++)
            {
                int length = _serviceTeamBundles[i].ServiceTeams.Length;
                if (max < length) max = length;
            }
        }
        else
        {
            max = _serviceTeamBundles[0].ServiceTeams.Length;
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
        ServiceTeamSO[] items = _serviceTeamBundles[index].ServiceTeams;
        for (int i = 0; i < items.Length; i++)
        {
            _slots[i].SetInfo(items[i]);
            _slots[i].gameObject.SetActive(true);
        }
    }
}
