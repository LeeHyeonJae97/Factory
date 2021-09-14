using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;

public class FactoryUI : MonoBehaviour
{
    [Header("Slot")]
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _holder;

    [SerializeField] private FactoryBundleSO _bundle; // have to change load asynchronously    

    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();

        _prefab.SetActive(false);
        for (int i = 0; i < _bundle.Factories.Length; i++)
        {
            int index = i;
            GameObject go = Instantiate(_prefab, _holder);
            go.GetComponent<FactorySlot>().SetInfo(_bundle.Factories[index]);
            go.SetActive(true);
        }
        _prefab.SetActive(true);
    }

    public void SetActive(bool value)
    {
        _canvas.SetActive(value);
    }
}
