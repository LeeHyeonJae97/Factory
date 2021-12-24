using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;

    public void SetInteractable(bool value)
    {
        for (int i = 0; i < _buttons.Length; i++)
            _buttons[i].interactable = value;
    }
}
