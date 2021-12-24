using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIExtension
{
    [RequireComponent(typeof(Graphic))]
    public class ColorAnimation : UIAnimation
    {
        [SerializeField] private Color _start = Color.white;
        [SerializeField] private Color _end = Color.white;
        [Min(0)] [SerializeField] private float _delay;
        [Min(0)] [SerializeField] private float _duration;
        [SerializeField] private bool _loop;
        [ShowIf("_loop")] [Indent(1)] [Min(-1)] [SerializeField] private int _loops;
        [ShowIf("_loop")] [Indent(1)] [SerializeField] private LoopType _loopType;
        [SerializeField] private Ease _ease;

        private Graphic _graphic;

        private void Awake()
        {
            if (_graphic == null) _graphic = GetComponent<Graphic>();
        }

        private void OnValidate()
        {
            if (_graphic == null) _graphic = GetComponent<Graphic>();
        }

        public override Tween Play()
        {
            return Play_Internal(_start, _end);
        }

        public override Tween PlayBackwards()
        {
            return Play_Internal(_end, _start);
        }

        private Tween Play_Internal(Color start, Color end)
        {
            _graphic.color = start;
            return _graphic.DOColor(end, _duration).SetEase(_ease).SetDelay(_delay).SetLoops(_loops, _loopType);
        }

        #region Utils

        [Button("Save Start", upperSpace: 5)]
        public override void SaveStart()
        {
            _start = _graphic.color;
        }

        [Button("Load Start")]
        public override void LoadStart()
        {
            _graphic.color = _start;
        }

        [Button("Save End", upperSpace: 5)]
        public override void SaveEnd()
        {
            _end = _graphic.color;
        }

        [Button("Load End")]
        public override void LoadEnd()
        {
            _graphic.color = _end;
        }

        #endregion
    }
}
