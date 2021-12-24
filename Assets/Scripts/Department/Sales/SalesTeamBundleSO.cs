using System.Collections;
using System.Collections.Generic;
using System.Data;
using TigerForge;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SalesTeamBundle", menuName = "ScriptableObject/SalesTeamBundle")]
public class SalesTeamBundleSO : ScriptableObject
{
    [field: SerializeField] public SalesTeamSO[] SalesTeams { get; private set; }
    [SerializeField] private ProductSO _input;
    [SerializeField] private MoneySO _output;
    [SerializeField] private MoneySO _upgradeOffering;
    [SerializeField] private CapabilitySO _fame;
    [SerializeField] private ChiefBundleSO _chiefBundle;

    public void Load(EasyFileSave file)
    {
        for (int i = 0; i < SalesTeams.Length; i++)
            SalesTeams[i].Load(file, _input, _output, _upgradeOffering, _fame, _chiefBundle);
    }

    public void Save(EasyFileSave file)
    {
        for (int i = 0; i < SalesTeams.Length; i++)
            SalesTeams[i].Save(file);
    }
}
