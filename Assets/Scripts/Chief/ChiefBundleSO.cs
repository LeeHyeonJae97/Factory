using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ChiefBundle", menuName = "ScriptableObject/ChiefBundle")]
public class ChiefBundleSO : ScriptableObject
{
    [field: SerializeField] public ChiefSO[] Chiefs { get; private set; }

    public float Buff
    {
        get
        {
            float value = 0;
            for (int i = 0; i < Chiefs.Length; i++)
                value += Chiefs[i].Buff * Chiefs[i].Amount;

            return value;
        }
    }

    public void Load(EasyFileSave file)
    {
        for (int i = 0; i < Chiefs.Length; i++)
            Chiefs[i].Load(file);
    }

    public void Save(EasyFileSave file)
    {
        for (int i = 0; i < Chiefs.Length; i++)
            Chiefs[i].Save(file);
    }
}
