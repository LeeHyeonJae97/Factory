using Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  CanvasActivator : MonoBehaviour
{
    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    public void SetActive(bool value)
    {
        _canvas.SetActive(value);
    }
}
