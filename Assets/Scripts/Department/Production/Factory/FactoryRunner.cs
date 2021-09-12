using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryRunner : MonoBehaviour
{
    [SerializeField] private FactoryBundleSO _bundle;

    private void Start()
    {
        Factory[] factories = _bundle.Factories;

        for (int i = 0; i < factories.Length; i++)
        {
            factories[i].Init(0); // Change to init with saved data

            if (factories[i].Level == 0)
                factories[i].onValueChanged += Produce;
            else
                StartCoroutine(factories[i].ProcessCoroutine());
        }
    }

    public void Produce(Factory factory)
    {
        if (factory.Level == 1)
        {
            StartCoroutine(factory.ProcessCoroutine());
            factory.onValueChanged -= Produce;
        }
    }
}
