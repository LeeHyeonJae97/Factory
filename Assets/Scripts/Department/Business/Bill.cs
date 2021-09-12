using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bill<T>
{
    [field: SerializeField] public T Asset { get; private set; }
    [field: SerializeField] public int Amount { get; protected set; }
}
