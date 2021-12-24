using Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepMode : MonoBehaviour
{
    [SerializeField] private Canvas[] _canvases;

    private Stack<Canvas> _enableds = new Stack<Canvas>();
    private Canvas _canvas;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

    public void SetActive(bool value)
    {
        if (value)
        {
            for (int i = 0; i < _canvases.Length; i++)
            {
                if (_canvases[i].enabled)
                {
                    // save enabled canvases to enable later
                    _enableds.Push(_canvases[i]);
                    _canvases[i].SetActive(false);
                }
            }
            _canvas.SetActive(true);
        }
        else
        {
            while (_enableds.Count > 0)
                _enableds.Pop().SetActive(true);
            _canvas.SetActive(false);
        }
    }
}
