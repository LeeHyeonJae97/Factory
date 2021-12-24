using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

[CreateAssetMenu(fileName = "ServiceTeamBundle", menuName = "ScriptableObject/ServiceTeamBundle")]
public class ServiceTeamBundleSO : ScriptableObject
{
    [field: SerializeField] public ServiceTeamSO[] ServiceTeams { get; private set; }
    [SerializeField] private CapabilitySO _output;
    [SerializeField] private MoneySO _upgradeOffering;
    [SerializeField] private CapabilitySO _fame;
    [SerializeField] private ChiefBundleSO _chiefBundle;

    public void Load(EasyFileSave file)
    {
        for (int i = 0; i < ServiceTeams.Length; i++)
            ServiceTeams[i].Load(file, _output, _upgradeOffering, _fame, _chiefBundle);
    }

    public void Save(EasyFileSave file)
    {
        for (int i = 0; i < ServiceTeams.Length; i++)
            ServiceTeams[i].Save(file);
    }
}
