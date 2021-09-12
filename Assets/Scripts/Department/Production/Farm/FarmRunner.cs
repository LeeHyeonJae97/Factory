using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmRunner : MonoBehaviour
{
    [SerializeField] private FarmBundleSO _bundle;

    private void Start()
    {
        Farm[] farms = _bundle.Farms;

        for (int i = 0; i < farms.Length; i++)
        {
            farms[i].Init(0); // Change to init with saved data

            if (farms[i].Level == 0)
                farms[i].onValueChanged += Produce;
            else
                StartCoroutine(farms[i].ProcessCoroutine());
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
