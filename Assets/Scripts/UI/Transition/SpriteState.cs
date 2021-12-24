using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using DG.Tweening;
using UnityEngine.Events;
using NaughtyAttributes;

namespace UIExtension
{
	[RequireComponent(typeof(Image))]
	public class SpriteState : UIState
	{		
		[Foldout("_stateName", true)]
		[SerializeField] private Sprite _value;

		private Image _target;

		private void OnValidate()
        {
			if (_target == null) _target = GetComponent<Image>();
        }

		public override Tween Transit(bool directly = false)
		{
			// Transit
			_target.sprite = _value;
			return null;
		}

		[Button("Save", upperSpace: 5)]
		public override void Save()
		{
			_value = _target.sprite;
		}

		[Button("Load")]
		public override void Load()
		{
			Undo.RecordObject(_target, "Load");
			_target.sprite = _value;
		}
	}
}
