using Extension;
using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

[CreateAssetMenu(fileName = "UserInfo", menuName = "ScriptableObject/UserInfo")]
public class UserInfoSO : ScriptableObject
{
    private static UserInfoSO _instance;
    public static UserInfoSO instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load<UserInfoSO>("UserInfo");

            return _instance;
        }
    }

    public string nickname;

    public void Load()
    {
        EasyFileSave file = new EasyFileSave("UserInfo");
        file.Load("UserInfo");

        StatPoint.Load(file);
        MoneySO.Load(file);
        ResourceSO.Load(file);
        ProductSO.Load(file);
        CapabilitySO.Load(file);
    }

    public void Save()
    {
        EasyFileSave file = new EasyFileSave("UserInfo");

        StatPoint.Save(file);
        MoneySO.Save(file);
        ResourceSO.Save(file);
        ProductSO.Save(file);
        CapabilitySO.Save(file);

        file.Save("UserInfo");
    }
}
