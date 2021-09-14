using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIExtension
{
    public class RadioGroup : MonoBehaviour
    {
        [SerializeField] private RadioButton[] _buttons;
        private RadioButton _selected;

        public void Start()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                int index = i;
                _buttons[i].onClick += () => Select(index);
                _buttons[i].OnStateChanged(false);
            }

            Select(0);
        }

        public void Select(int index)
        {
            if (_buttons[index] == _selected)
                return;

            if (_selected != null)
                _selected.OnStateChanged(false);

            _selected = _buttons[index];
            _selected.OnStateChanged(true);
        }
    }
}