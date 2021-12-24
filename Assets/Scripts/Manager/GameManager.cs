using Extension;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UIExtension;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FarmBundleSO[] _farmBundles;
    [SerializeField] private FactoryBundleSO[] _factoryBundles;
    [SerializeField] private SalesTeamBundleSO[] _salesTeamBundles;
    [SerializeField] private ServiceTeamBundleSO[] _serviceTeamBundles;
    [SerializeField] private ChiefBundleSO[] _chiefBundles;
    [SerializeField] private SettingsSO _settings;

    [SerializeField] private RadioGroup _radioGroup;    

    private EasyFileSave _file;

    private void Awake()
    {
        // load UserInfo(money, resource, capability)
        UserInfoSO.instance.Load();

        // load Abilities
        _file = new EasyFileSave("Ability");
        _file.Load("Ability");
        AbilitySO.Load(_file);

        // load FarmBundles
        _file = new EasyFileSave("Farm");
        _file.Load("Farm");
        for (int i = 0; i < _farmBundles.Length; i++)
            _farmBundles[i].Load(_file);

        // load FactoryBundles
        _file = new EasyFileSave("Factory");
        _file.Load("Factory");
        for (int i = 0; i < _factoryBundles.Length; i++)
            _factoryBundles[i].Load(_file);

        // load SalesTeamBundles
        _file = new EasyFileSave("SalesTeam");
        _file.Load("SalesTeam");
        for (int i = 0; i < _salesTeamBundles.Length; i++)
            _salesTeamBundles[i].Load(_file);

        // load ServiceTeamBundles
        _file = new EasyFileSave("ServiceTeam");
        _file.Load("ServiceTeam");
        for (int i = 0; i < _serviceTeamBundles.Length; i++)
            _serviceTeamBundles[i].Load(_file);

        // load ChiefBundles
        _file = new EasyFileSave("Chief");
        _file.Load("Chief");
        for (int i = 0; i < _chiefBundles.Length; i++)
            _chiefBundles[i].Load(_file);

        // load settings
        _settings.Load();
    }

    private IEnumerator Start()
    {
        yield return null;
        _radioGroup.Select(0);
    }

    private void OnApplicationQuit()
    {
        // load UserInfo(money, resource, capability)
        UserInfoSO.instance.Save();

        // load Abilities
        _file = new EasyFileSave("Ability");
        AbilitySO.Load(_file);
        _file.Save("Ability");

        // load FarmBundles
        _file = new EasyFileSave("Farm");
        for (int i = 0; i < _farmBundles.Length; i++)
            _farmBundles[i].Save(_file);
        _file.Save("Farm");

        // load FactoryBundles
        _file = new EasyFileSave("Factory");
        for (int i = 0; i < _factoryBundles.Length; i++)
            _factoryBundles[i].Save(_file);
        _file.Save("Factory");

        // load SalesTeamBundles
        _file = new EasyFileSave("SalesTeam");
        for (int i = 0; i < _salesTeamBundles.Length; i++)
            _salesTeamBundles[i].Save(_file);
        _file.Save("SalesTeam");

        // save ServiceTeamBundles
        _file = new EasyFileSave("ServiceTeam");
        for (int i = 0; i < _salesTeamBundles.Length; i++)
            _serviceTeamBundles[i].Save(_file);
        _file.Save("ServiceTeam");

        // save ChiefBundles
        _file = new EasyFileSave("Chief");
        for (int i = 0; i < _chiefBundles.Length; i++)
            _chiefBundles[i].Save(_file);
        _file.Save("Chief");

        // save settings
        _settings.Save();
    }
}
