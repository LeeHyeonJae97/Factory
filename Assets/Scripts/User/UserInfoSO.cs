using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserInfo", menuName = "ScriptableObject/UserInfo")]
public class UserInfoSO : ScriptableObject
{
    [field: SerializeField] public FameSO Fame { get; private set; }
    [field: SerializeField] public AssetSO[] Assets { get; private set; }
    [field: SerializeField] public ProductSO[] Products { get; private set; }

    public void Init()
    {
        // Load data using EasyFileSave

        Fame.Init(1, 0);
        for (int i = 0; i < Assets.Length; i++)
            Assets[i].Init(0);
        for (int i = 0; i < Products.Length; i++)
            Products[i].Init(0);
    }
}
