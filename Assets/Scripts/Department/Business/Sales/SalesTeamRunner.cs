using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesTeamRunner : MonoBehaviour
{
    [SerializeField] private SalesTeamBundleSO _bundle;

    private void Start()
    {
        SalesTeam[] salesTeams = _bundle.SalesTeams;

        for (int i = 0; i < salesTeams.Length; i++)
        {            
            salesTeams[i].Init(0); // Change to init with saved data

            if (salesTeams[i].Level == 0)
                salesTeams[i].onValueChanged += Sell;
            else
                StartCoroutine(salesTeams[i].ProcessCoroutine());
        }
    }

    public void Sell(SalesTeam salesTeam)
    {
        if (salesTeam.Level == 1)
        {
            StartCoroutine(salesTeam.ProcessCoroutine());
            salesTeam.onValueChanged -= Sell;
        }
    }
}
