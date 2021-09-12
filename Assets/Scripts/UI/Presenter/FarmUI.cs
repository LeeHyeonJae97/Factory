using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmUI : MonoBehaviour
{
    [SerializeField] private FarmSlot _prefab;
    [SerializeField] private Transform _holder;

    [SerializeField] private FarmBundleSO _bundle; // have to change load asynchronously    

    private void Awake()
    {
        for (int i = 0; i < _bundle.Farms.Length; i++)
        {
            int index = i;
            Instantiate(_prefab, _holder).Init(_bundle.Farms[index]);
        }
    }
}
