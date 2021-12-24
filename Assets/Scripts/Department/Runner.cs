using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    [SerializeField] private FarmBundleSO[] _farmBundles;
    [SerializeField] private FactoryBundleSO[] _factoryBundles;
    [SerializeField] private SalesTeamBundleSO[] _salesTeamBundles;
    [SerializeField] private ServiceTeamBundleSO[] _serviceTeamBundles;

    private void Start()
    {
        // Run farm
        for (int i = 0; i < _farmBundles.Length; i++)
        {
            FarmSO[] farms = _farmBundles[i].Farms;

            for (int j = 0; j < farms.Length; j++)
            {
                if (farms[j].Level == 0)
                    farms[j].onValueChanged += Produce;
                else
                    StartCoroutine(farms[j].ProcessCoroutine());
            }
        }

        // Run factory
        for (int i = 0; i < _factoryBundles.Length; i++)
        {
            FactorySO[] factories = _factoryBundles[i].Factories;

            for (int j = 0; j < factories.Length; j++)
            {
                if (factories[j].Level == 0)
                    factories[j].onValueChanged += Produce;
                else
                    StartCoroutine(factories[j].ProcessCoroutine());
            }
        }

        // Run sales team
        for (int i = 0; i < _salesTeamBundles.Length; i++)
        {
            SalesTeamSO[] salesTeams = _salesTeamBundles[i].SalesTeams;

            for (int j = 0; j < salesTeams.Length; j++)
            {
                if (salesTeams[j].Level == 0)
                    salesTeams[j].onValueChanged += Sell;
                else
                    StartCoroutine(salesTeams[j].ProcessCoroutine());
            }
        }

        // Run service team
        for (int i = 0; i < _serviceTeamBundles.Length; i++)
        {
            ServiceTeamSO[] serviceTeams = _serviceTeamBundles[i].ServiceTeams;

            for (int j = 0; j < serviceTeams.Length; j++)
            {
                if (serviceTeams[j].Level == 0)
                    serviceTeams[j].onValueChanged += Service;
                else
                    StartCoroutine(serviceTeams[j].ProcessCoroutine());
            }
        }
    }

    // Start producing resources
    private void Produce(FarmSO farm)
    {
        if (farm.Level == 1)
        {
            StartCoroutine(farm.ProcessCoroutine());
            farm.onValueChanged -= Produce;
        }
    }

    // Start producing products
    private void Produce(FactorySO factory)
    {
        if (factory.Level == 1)
        {
            StartCoroutine(factory.ProcessCoroutine());
            factory.onValueChanged -= Produce;
        }
    }

    // Start selling products
    public void Sell(SalesTeamSO salesTeam)
    {
        if (salesTeam.Level == 1)
        {
            StartCoroutine(salesTeam.ProcessCoroutine());
            salesTeam.onValueChanged -= Sell;
        }
    }

    // Start servicing customers
    public void Service(ServiceTeamSO serviceTeam)
    {
        if (serviceTeam.Level == 1)
        {
            StartCoroutine(serviceTeam.ProcessCoroutine());
            serviceTeam.onValueChanged -= Service;
        }
    }
}
