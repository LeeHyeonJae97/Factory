using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;

public class FactoryUI : MonoBehaviour, ITab
{
    [Header("Slot")]
    [SerializeField] protected FactorySlot _slotPrefab;
    [SerializeField] protected Transform _slotHolder;
    [SerializeField] protected FactoryBundleSO[] _factoryBundles; // have to change load asynchronously        

    [Header("Chief")]
    [SerializeField] protected ChiefSlot[] _chiefSlots;
    [SerializeField] protected ChiefBundleSO _chiefBundle;

    protected List<FactorySlot> _slots = new List<FactorySlot>();

    private void Awake()
    {
        // get max count of farms in bundle
        int max = 0;
        if (_factoryBundles.Length > 1)
        {
            for (int i = 0; i < _factoryBundles.Length; i++)
            {
                int length = _factoryBundles[i].Factories.Length;
                if (max < length) max = length;
            }
        }
        else
        {
            max = _factoryBundles[0].Factories.Length;
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

        // set factory info
        FactorySO[] items = _factoryBundles[index].Factories;
        for (int i = 0; i < items.Length; i++)
        {
            _slots[i].SetInfo(items[i]);
            _slots[i].gameObject.SetActive(true);
        }
    }
}
