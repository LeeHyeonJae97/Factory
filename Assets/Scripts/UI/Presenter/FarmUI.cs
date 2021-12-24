using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmUI : MonoBehaviour, ITab
{
    [Header("Slot")]
    [SerializeField] protected FarmSlot _slotPrefab;
    [SerializeField] protected Transform _slotHolder;
    [SerializeField] protected FarmBundleSO[] _farmBundles; // have to change load asynchronously        

    [Header("Chief")]
    [SerializeField] protected ChiefSlot[] _chiefSlots;
    [SerializeField] protected ChiefBundleSO _chiefBundle;

    protected List<FarmSlot> _slots = new List<FarmSlot>();

    private void Awake()
    {
        // get max count of farms in bundle
        int max = 0;
        if (_farmBundles.Length > 1)
        {
            for (int i = 0; i < _farmBundles.Length; i++)
            {
                int length = _farmBundles[i].Farms.Length;
                if (max < length) max = length;
            }
        }
        else
        {
            max = _farmBundles[0].Farms.Length;
        }

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
        FarmSO[] items = _farmBundles[index].Farms;
        for (int i = 0; i < items.Length; i++)
        {
            _slots[i].SetInfo(items[i]);
            _slots[i].gameObject.SetActive(true);
        }
    }
}
