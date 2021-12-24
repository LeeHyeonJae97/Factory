using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Events;
using DG.Tweening;
using NaughtyAttributes;

namespace UIExtension
{
	[RequireComponent(typeof(Graphic))]
	public class ColorState : UIState
	{
		[Foldout("_stateName", true)]
		[SerializeField] private Color _value = Color.white;
		[Foldout("_stateName", true)]
		[Min(0)]
		[SerializeField] private float _duration;
		[Foldout("_stateName", true)]
		[SerializeField] private Ease _ease;

		private Graphic _target;

		private void OnValidate()
		{
			if(_target == null) _target = GetComponent<Graphic>();
		}

		public override Tween Transit(bool directly = false)
		{
			// Transit
			Tween tween = _target.DOColor(_value, directly ? 0 : _duration).SetEase(_ease).SetDelay(_delay);
			return tween;
		}

		[Button("Save", upperSpace: 5)]
		public override void Save()
		{
			_value = _target.color;
		}

		[Button("Load")]
		public override void Load()
		{
			Undo.RecordObject(_target, "Load");
			_target.color = _value;
		}
	}
}
