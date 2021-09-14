using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Management : MonoBehaviour
{
    public List<ChiefBundleSO> ChiefFarmingOfficers { get; private set; } = new List<ChiefBundleSO>();
    public List<ChiefBundleSO> ChiefProductionOfficers { get; private set; } = new List<ChiefBundleSO>();
    public List<ChiefBundleSO> ChiefSalesOfficers { get; private set; } = new List<ChiefBundleSO>();

    public void Init()
    {

    }

    public void Scout(ChiefFarmingOfficerSO cfo)
    {
        ChiefFarmingOfficers.Add(cfo);

        // Apply buff
    }

    public void Scout(ChiefProductionOfficerSO cpo)
    {
        ChiefProductionOfficers.Add(cpo);

        // Apply buff
    }

    public void Scout(ChiefSalesOfficerSO cso)
    {
        ChiefProductionOfficers.Add(cso);

        // Apply buff
    }
}
