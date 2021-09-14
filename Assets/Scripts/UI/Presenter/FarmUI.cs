using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;

public class FarmUI : MonoBehaviour
{
    [Header("Slot")]
    [SerializeField] private FarmSlot _farmSlotPrefab;    
    [SerializeField] private Transform _farmSlotholder;
    [SerializeField] private FarmBundleSO[] _farmBundles; // have to change load asynchronously        

    [Header("Chief")]
    [SerializeField] private ChiefSlot[] _chiefSlots;
    [SerializeField] private ChiefUI _chiefUI;
    [SerializeField] private ChiefBundleSO[] _chiefBundles;

    private Canvas _canvas;
    private List<FarmSlot> _farmSlots = new List<FarmSlot>();

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();

        // get max count of farms in bundle
        int max = 0;
        for (int i = 0; i < _farmBundles.Length; i++)
        {
            int length = _farmBundles[i].Farms.Length;
            if (max < length) max = length;
        }

        // instantiate max count of farm slots
        _farmSlotPrefab.gameObject.SetActive(false);
        for (int i = 0; i < max; i++)
            _farmSlots.Add(Instantiate(_farmSlotPrefab, _farmSlotholder));
        _farmSlotPrefab.gameObject.SetActive(true);

        // add click listeners to chief slots
        for (int i = 0; i < _chiefSlots.Length; i++)
            _chiefSlots[i].Init(_chiefUI.Open);
    }

    public void Select(int index)
    {
        // set index farm bundle info
        Farm[] farms = _farmBundles[index].Farms;
        for (int i = 0; i < _farmSlots.Count; i++)
        {
            if (i < farms.Length)
            {
                _farmSlots[i].SetInfo(farms[i]);
                _farmSlots[i].gameObject.SetActive(true);
            }
            else
            {
                _farmSlots[i].gameObject.SetActive(false);
            }
        }

        // set index chief bundle info
        Chief[] chiefs = _chiefBundles[index].Chiefs;
        for (int i = 0; i < _chiefSlots.Length; i++)
            _chiefSlots[i].SetInfo(chiefs[i]);
    }

    public void SetActive(bool value)
    {
        _canvas.SetActive(value);
    }
}
