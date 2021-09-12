using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryUI : MonoBehaviour
{
    [SerializeField] private FactorySlot _prefab;
    [SerializeField] private Transform _holder;

    [SerializeField] private FactoryBundleSO _bundle; // have to change load asynchronously    

    private void Awake()
    {
        for (int i = 0; i < _bundle.Factories.Length; i++)
        {
            int index = i;
            Instantiate(_prefab, _holder).Init(_bundle.Factories[index]);
        }
    }
}
