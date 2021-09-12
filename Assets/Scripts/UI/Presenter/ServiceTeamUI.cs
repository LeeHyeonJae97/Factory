using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceTeamUI : MonoBehaviour
{
    [SerializeField] private ServiceTeamSlot _prefab;
    [SerializeField] private Transform _holder;

    [SerializeField] private ServiceTeamBundleSO _bundle; // have to change load asynchronously    

    private void Awake()
    {
        for (int i = 0; i < _bundle.ServiceTeams.Length; i++)
        {
            int index = i;
            Instantiate(_prefab, _holder).Init(_bundle.ServiceTeams[index]);
        }
    }
}
