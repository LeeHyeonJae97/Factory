using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace UIExtension
{
	public class UIAnimationController : MonoBehaviour
	{
		private enum Reference { OnlyChildren, Manually }

		[Space(10)] [SerializeField] private Reference _reference;
		[ShowIf("_reference", Reference.Manually)] [SerializeField] private UIAnimation[] _animations;

		private Sequence _sequence;

		[Space(5)] public UnityEvent onStart;
		public UnityEvent onFinished;

		public bool IsPlaying => _sequence.IsActive();

		private void OnValidate()
		{
			if (_reference == Reference.OnlyChildren) _animations = GetComponentsInChildren<UIAnimation>();
		}

		public void Play(bool backwards = false)
		{
			if (_animations == null || _animations.Length == 0)
			{
				Debug.LogWarning("No animations");
				return;
			}

			if (_sequence.IsActive()) _sequence.Kill();

			_sequence = DOTween.Sequence();

			onStart?.Invoke();
			for (int i = 0; i < _animations.Length; i++)
			{
				if (!backwards)
					_sequence.Join(_animations[i].Play());
				else
					_sequence.Join(_animations[i].PlayBackwards());
			}

			_sequence.onComplete += () => onFinished?.Invoke();
		}

		[Button]
		public void Test()
        {
			Play();
        }

		[Button]
		public void TestBackwards()
        {
			Play(true);
        }
	}
}