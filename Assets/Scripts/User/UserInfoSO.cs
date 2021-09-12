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


    }
}
