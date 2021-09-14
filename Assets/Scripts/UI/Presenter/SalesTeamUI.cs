using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;

public class SalesTeamUI : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _holder;

    [SerializeField] private SalesTeamBundleSO _bundle; // have to change load asynchronously     

    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();

        _prefab.SetActive(false);
        for (int i = 0; i < _bundle.SalesTeams.Length; i++)
        {
            int index = i;
            GameObject go = Instantiate(_prefab, _holder);
            go.GetComponent<SalesTeamSlot>().SetInfo(_bundle.SalesTeams[index]);
            go.SetActive(true);
        }
        _prefab.SetActive(true);
    }

    public void SetActive(bool value)
    {
        _canvas.SetActive(value);
    }
}
