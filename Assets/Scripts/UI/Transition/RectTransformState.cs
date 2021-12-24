using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UIExtension
{
	public class RectTransformState : UIState
	{
		public enum Type { AnchoredPosition, Position, Rotation, Scale, SizeDelta }

		[Foldout("_stateName", true)]
		[SerializeField] private Type _type;
		[Foldout("_stateName", true)]
		[SerializeField] private Vector3 _value;
		[Foldout("_stateName", true)]
		[Min(0)]
		[SerializeField] private float _duration;
		[Foldout("_stateName", true)]
		[SerializeField] private Ease _ease;
		[ShowIf("_type", Type.Rotation)] [SerializeField] private RotateMode _rotateMode;

		private RectTransform _rectTr;

		private void OnValidate()
		{
			if (_rectTr == null) _rectTr = (RectTransform)transform;
		}

		public override Tween Transit(bool directly = false)
		{
			switch (_type)
			{
				case Type.AnchoredPosition:
					return _rectTr.DOAnchorPos(_value, directly ? 0 : _duration).SetEase(_ease).SetDelay(_delay);

				case Type.Position:
					return _rectTr.DOMove(_value, directly ? 0 : _duration).SetEase(_ease).SetDelay(_delay);

				case Type.Rotation:
					return _rectTr.DORotate(_value, directly ? 0 : _duration, _rotateMode).SetDelay(_delay);

				case Type.Scale:
					return _rectTr.DOScale(_value, directly ? 0 : _duration).SetEase(_ease).SetDelay(_delay);

				case Type.SizeDelta:
					return _rectTr.DOSizeDelta(_value, directly ? 0 : _duration).SetEase(_ease).SetDelay(_delay);

				default:
					Debug.Log("Wrong type");
					return null;
			}
		}

		[Button("Save", upperSpace: 5)]
		public override void Save()
		{
			Undo.RecordObject(this, "Save");

			switch (_type)
			{
				case Type.AnchoredPosition:
					_value = _rectTr.anchoredPosition;
					break;

				case Type.Position:
					_value = _rectTr.position;
					break;

				case Type.Rotation:
					_value = _rectTr.rotation.eulerAngles;
					break;

				case Type.Scale:
					_value = _rectTr.localScale;
					break;

				case Type.SizeDelta:
					_value = _rectTr.sizeDelta;
					break;

				default:
					Debug.Log("Wrong type");
					break;
			}
		}

		[Button("Load")]
		public override void Load()
		{
			Undo.RecordObject(_rectTr, "Load");

			switch (_type)
			{
				case Type.AnchoredPosition:
					_rectTr.anchoredPosition = _value;
					break;

				case Type.Position:
					_rectTr.position = _value;
					break;

				case Type.Rotation:
					_rectTr.rotation = Quaternion.Euler(_value);
					break;

				case Type.Scale:
					_rectTr.localScale = _value;
					break;

				case Type.SizeDelta:
					_rectTr.sizeDelta = _value;
					break;

				default:
					Debug.Log("Wrong type");
					break;
			}
		}
	}
}