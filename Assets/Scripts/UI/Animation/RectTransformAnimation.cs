using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UIExtension
{
    public class RectTransformAnimation : UIAnimation
    {
        public enum Type { AnchoredPosition, Position, Rotation, Scale, SizeDelta }

        [SerializeField] private Type _type;
        [SerializeField] private Vector3 _start;
        [SerializeField] private Vector3 _end;
        [Min(0)] [SerializeField] private float _delay;
        [Min(0)] [SerializeField] private float _duration;
        [SerializeField] private bool _loop;
        [ShowIf("_loop")] [Indent(1)] [Min(-1)] [SerializeField] private int _loops;
        [ShowIf("_loop")] [Indent(1)] [SerializeField] private LoopType _loopType;
        [SerializeField] private Ease _ease;
        [ShowIf("_type", Type.Rotation)] [SerializeField] private RotateMode _rotateMode;

        private RectTransform _rectTr;

        private void Awake()
        {
            if (_rectTr == null) _rectTr = (RectTransform)transform;
            if (!_loop) _loops = 0;
        }

        private void OnValidate()
        {
            if (_rectTr == null) _rectTr = (RectTransform)transform;
            if (!_loop) _loops = 0;
        }

        public override Tween Play()
        {
            return Play_Internal(_start, _end);
        }

        public override Tween PlayBackwards()
        {
            return Play_Internal(_end, _start);
        }

        private Tween Play_Internal(Vector3 start, Vector3 end)
        {
            switch (_type)
            {
                case Type.AnchoredPosition:
                    _rectTr.anchoredPosition = start;
                    return _rectTr.DOAnchorPos(end, _duration).SetEase(_ease).SetDelay(_delay).SetLoops(_loops, _loopType);

                case Type.Position:
                    _rectTr.position = start;
                    return _rectTr.DOMove(end, _duration).SetEase(_ease).SetDelay(_delay).SetLoops(_loops, _loopType);

                case Type.Rotation:
                    _rectTr.rotation = Quaternion.Euler(start);
                    return _rectTr.DORotate(end, _duration, _rotateMode).SetEase(_ease).SetDelay(_delay).SetLoops(_loops, _loopType);

                case Type.Scale:
                    _rectTr.localScale = start;
                    return _rectTr.DOScale(end, _duration).SetEase(_ease).SetDelay(_delay).SetLoops(_loops, _loopType);

                case Type.SizeDelta:
                    _rectTr.sizeDelta = start;
                    return _rectTr.DOSizeDelta(end, _duration).SetEase(_ease).SetDelay(_delay).SetLoops(_loops, _loopType);

                default:
                    Debug.Log("Wrong type");
                    return null;
            }
        }

        #region Utils

        [Button("Save Start", upperSpace: 5)]
        public override void SaveStart()
        {
            Undo.RecordObject(this, "Save");

            switch (_type)
            {
                case Type.AnchoredPosition:
                    _start = _rectTr.anchoredPosition;
                    break;

                case Type.Position:
                    _start = _rectTr.position;
                    break;

                case Type.Rotation:
                    _start = _rectTr.rotation.eulerAngles;
                    break;

                case Type.Scale:
                    _start = _rectTr.localScale;
                    break;

                case Type.SizeDelta:
                    _start = _rectTr.sizeDelta;
                    break;

                default:
                    Debug.Log("Wrong type");
                    break;
            }
        }

        [Button("Load Start")]
        public override void LoadStart()
        {
            Undo.RecordObject(_rectTr, "Load");

            switch (_type)
            {
                case Type.AnchoredPosition:
                    _rectTr.anchoredPosition = _start;
                    break;

                case Type.Position:
                    _rectTr.position = _start;
                    break;

                case Type.Rotation:
                    _rectTr.rotation = Quaternion.Euler(_start);
                    break;

                case Type.Scale:
                    _rectTr.localScale = _start;
                    break;

                case Type.SizeDelta:
                    _rectTr.sizeDelta = _start;
                    break;

                default:
                    Debug.Log("Wrong type");
                    break;
            }
        }

        [Button("Save End", upperSpace: 5)]
        public override void SaveEnd()
        {
            Undo.RecordObject(this, "Save");

            switch (_type)
            {
                case Type.AnchoredPosition:
                    _end = _rectTr.anchoredPosition;
                    break;

                case Type.Position:
                    _end = _rectTr.position;
                    break;

                case Type.Rotation:
                    _end = _rectTr.rotation.eulerAngles;
                    break;

                case Type.Scale:
                    _end = _rectTr.localScale;
                    break;

                case Type.SizeDelta:
                    _end = _rectTr.sizeDelta;
                    break;

                default:
                    Debug.Log("Wrong type");
                    break;
            }
        }

        [Button("Load End")]
        public override void LoadEnd()
        {
            Undo.RecordObject(_rectTr, "Load");

            switch (_type)
            {
                case Type.AnchoredPosition:
                    _rectTr.anchoredPosition = _end;
                    break;

                case Type.Position:
                    _rectTr.position = _end;
                    break;

                case Type.Rotation:
                    _rectTr.rotation = Quaternion.Euler(_end);
                    break;

                case Type.Scale:
                    _rectTr.localScale = _end;
                    break;

                case Type.SizeDelta:
                    _rectTr.sizeDelta = _end;
                    break;

                default:
                    Debug.Log("Wrong type");
                    break;
            }
        }

        #endregion
    }
}