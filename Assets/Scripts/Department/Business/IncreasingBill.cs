using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[System.Serializable]
public class IncreasingBill<T> : Bill<T>
{
    [field: SerializeField] public string Formula { get; private set; }

    public void SetAmount(int level)
    {
        Amount = int.Parse(new DataTable().Compute(string.Format(Formula, level), null).ToString());
    }
}
