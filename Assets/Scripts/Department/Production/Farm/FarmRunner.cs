using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmRunner : MonoBehaviour
{
    [SerializeField] private FarmBundleSO[] _bundles;

    private void Start()
    {
        for (int i = 0; i < _bundles.Length; i++)
        {
            Farm[] farms = _bundles[i].Farms;

            for (int j = 0; j < farms.Length; j++)
            {
                farms[j].Init(0); // Change to init with saved data

                if (farms[j].Level == 0)
                    farms[j].onValueChanged += Produce;
                else
                    StartCoroutine(farms[j].ProcessCoroutine());
            }
        }
    }

    public void Produce(Farm farm)
    {
        if (farm.Level == 1)
        {
            StartCoroutine(farm.ProcessCoroutine());
            farm.onValueChanged -= Produce;
        }
    }
}
