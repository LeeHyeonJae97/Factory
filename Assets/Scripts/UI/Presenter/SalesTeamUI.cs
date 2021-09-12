using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesTeamUI : MonoBehaviour
{
    [SerializeField] private SalesTeamSlot _prefab;
    [SerializeField] private Transform _holder;

    [SerializeField] private SalesTeamBundleSO _bundle; // have to change load asynchronously    

    private void Awake()
    {
        for (int i = 0; i < _bundle.SalesTeams.Length; i++)
        {
            int index = i;
            Instantiate(_prefab, _holder).Init(_bundle.SalesTeams[index]);
        }
    }
}
