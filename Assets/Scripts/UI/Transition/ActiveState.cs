using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NaughtyAttributes;
using DG.Tweening;

namespace UIExtension
{
	public class ActiveState : UIState
	{
		[Foldout("_stateName", true)]
		[SerializeField] private bool _active;

		public override Tween Transit(bool directly = false)
		{
			// Transit
			gameObject.SetActive(_active);
			return null;
		}

		[Button("Save", upperSpace: 5)]
		public override void Save()
		{
			_active = gameObject.activeSelf;
		}

		[Button("Load")]
		public override void Load()
		{
			Undo.RecordObject(gameObject, "Load");
			gameObject.SetActive(_active);
		}
	}
}
