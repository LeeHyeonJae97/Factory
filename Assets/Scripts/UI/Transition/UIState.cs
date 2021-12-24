using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using NaughtyAttributes;

namespace UIExtension
{
	public abstract class UIState : MonoBehaviour
	{
		[Foldout("_stateName", true)]
		[SerializeField] protected string _stateName;
		public string StateName => _stateName;

		[Foldout("_stateName", true)]
		[SerializeField] protected float _delay;

		public abstract Tween Transit(bool directly = false);
		public abstract void Save();
		public abstract void Load();
	}
}