using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;

public class ShopUI : MonoBehaviour
{
    private Canvas _canvas;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

    public void SetActive(bool value)
    {
        _canvas.SetActive(value);
    }
}
