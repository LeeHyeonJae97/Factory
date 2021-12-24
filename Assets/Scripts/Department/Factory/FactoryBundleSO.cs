using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TigerForge;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FactoryBundle", menuName = "ScriptableObject/FactoryBundle")]
public class FactoryBundleSO : ScriptableObject
{
    [field: SerializeField] public FactorySO[] Factories { get; private set; }
    [SerializeField] private ResourceSO[] _inputs;
    [SerializeField] private ProductSO _output;
    [SerializeField] private MoneySO _upgradeOffering;
    [SerializeField] private CapabilitySO _fame;
    [SerializeField] private ChiefBundleSO _chiefBundle;

    public void Load(EasyFileSave file)
    {
        for (int i = 0; i < Factories.Length; i++)
            Factories[i].Load(file, _inputs, _output, _upgradeOffering, _fame, _chiefBundle);
    }

    public void Save(EasyFileSave file)
    {
        for (int i = 0; i < Factories.Length; i++)
            Factories[i].Save(file);
    }
}