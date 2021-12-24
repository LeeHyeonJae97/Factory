using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObject/Settings")]
public class SettingsSO : ScriptableObject
{
    public bool muteBGM;
    public bool muteSFX;
    public bool receivePushMessage;

    [Button]
    public void TestA()
    {
        muteBGM = !muteBGM;
    }

    [Button]
    public void TestB()
    {
        muteSFX = !muteSFX;
    }

    [Button]
    public void TestC()
    {
        receivePushMessage = !receivePushMessage;
    }

    public void Load()
    {
        EasyFileSave file = new EasyFileSave("Settings");
        file.Load("Settings");
        muteBGM = file.GetBool("MuteBGM");
        muteSFX = file.GetBool("MuteSFX");
        receivePushMessage = file.GetBool("ReceivePushMessage");
    }

    public void Save()
    {
        EasyFileSave file = new EasyFileSave("Settings");
        file.Add("MuteBGM", muteBGM);
        file.Add("MuteSFX", muteSFX);
        file.Add("ReceivePushMessage", receivePushMessage);
        file.Save("Settings");
    }
}
