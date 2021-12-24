using System.Collections;
using System.Collections.Generic;
using System.Data;
using TigerForge;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FarmBundle", menuName = "ScriptableObject/FarmBundle")]
public class FarmBundleSO : ScriptableObject
{
    [field: SerializeField] public FarmSO[] Farms { get; private set; }
    [SerializeField] private ResourceSO _output;
    [SerializeField] private MoneySO _upgradeOffering;
    [SerializeField] private CapabilitySO _fame;
    [SerializeField] private ChiefBundleSO _chiefBundle;

    public void Load(EasyFileSave file)
    {
        for (int i = 0; i < Farms.Length; i++)
            Farms[i].Load(file, _output, _upgradeOffering, _fame, _chiefBundle);
    }

    public void Save(EasyFileSave file)
    {
        for (int i = 0; i < Farms.Length; i++)
            Farms[i].Save(file);
    }
}
