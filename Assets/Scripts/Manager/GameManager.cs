using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UserInfoSO _userInfo;
    [SerializeField] private FarmBundleSO[] _farmBundles;
    [SerializeField] private FactoryBundleSO _factoryBundle;
    [SerializeField] private SalesTeamBundleSO _salesTeamBundle;
    [SerializeField] private ServiceTeamBundleSO _serviceTeamBundle;

    private void Start()
    {
        _userInfo.Init();
        for (int i = 0; i < _farmBundles.Length; i++)
            _farmBundles[i].Init();
        _factoryBundle.Init();
        _salesTeamBundle.Init();
        _serviceTeamBundle.Init();
    }
}
