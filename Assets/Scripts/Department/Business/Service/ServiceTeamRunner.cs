using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceTeamRunner : MonoBehaviour
{
    [SerializeField] private ServiceTeamBundleSO _bundle;

    private void Start()
    {
        ServiceTeam[] serviceTeams = _bundle.ServiceTeams;

        for (int i = 0; i < serviceTeams.Length; i++)
        {
            serviceTeams[i].Init(0); // Change to init with saved data

            if (serviceTeams[i].Level == 0)
                serviceTeams[i].onValueChanged += Sell;
            else
                StartCoroutine(serviceTeams[i].ProcessCoroutine());
        }
    }

    public void Sell(ServiceTeam serviceTeam)
    {
        if (serviceTeam.Level == 1)
        {
            StartCoroutine(serviceTeam.ProcessCoroutine());
            serviceTeam.onValueChanged -= Sell;
        }
    }
}
