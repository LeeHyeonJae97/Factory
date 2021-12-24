using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;
using UnityEngine.Events;
using System.Linq;

namespace UIExtension
{
	public class UITransitionController : MonoBehaviour
	{
		[System.Serializable]
		public class UITransition
		{
			[AllowNesting] [ReadOnly] [SerializeField] public string stateName;
			[SerializeField] public List<UIState> states = new List<UIState>();

			[Space(5)] public UnityEvent onStart;
			public UnityEvent onFinished;

			public UITransition(string stateName)
			{
				this.stateName = stateName;
			}
		}

		private enum Reference { OnlyChildren, Manually }

		[SerializeField] private string _initialStateName;
		[ReadOnly] [SerializeField] private string _currentStateName;

		[Space(10)] [SerializeField] private Reference _reference;

		[SerializeField] private List<UITransition> _transitions = new List<UITransition>();
		private Dictionary<string, UITransition> _transitionDic = new Dictionary<string, UITransition>();

		private Sequence _sequence;

		public bool IsInTransition => _sequence.IsActive();

		private void Start()
		{
			if (!string.IsNullOrEmpty(_initialStateName))
				TransitDirectly(_initialStateName);

			_currentStateName = _initialStateName;
		}

		private void OnValidate()
		{
			if (_reference == Reference.OnlyChildren)
			{
				// refill dictionary when compiled
				if (this._transitionDic == null || this._transitionDic.Count == 0)
				{
					for (int i = 0; i < this._transitions.Count; i++)
						this._transitionDic.Add(this._transitions[i].stateName, this._transitions[i]);
				}

				List<UITransition> transitions = new List<UITransition>();
				Dictionary<string, UITransition> transitionDic = new Dictionary<string, UITransition>();

				UIState[] states = GetComponentsInChildren<UIState>();

				for (int i = 0; i < states.Length; i++)
				{
					if (this._transitionDic.ContainsKey(states[i].StateName))
					{
						// use existing transition
						UITransition transition = this._transitionDic[states[i].StateName];

						if (!transitionDic.ContainsKey(states[i].StateName))
						{
							// remove all old and deleted states
							transition.states.RemoveAll((o) => !states.Contains(o) || !transition.stateName.Equals(o.StateName));

							transitions.Add(transition);
							transitionDic.Add(transition.stateName, transition);
						}

						// add state
						if (!transition.states.Contains(states[i])) transition.states.Add(states[i]);
					}
					else
					{
						// add new transition
						if (!transitionDic.ContainsKey(states[i].StateName))
						{
							UITransition transition = new UITransition(states[i].StateName);
							transitions.Add(transition);
							transitionDic.Add(transition.stateName, transition);
						}

						// add state
						transitionDic[states[i].StateName].states.Add(states[i]);
					}
				}

				// update transition list and dictionary
				this._transitions = transitions;
				this._transitionDic = transitionDic;
			}
			else
			{
				Debug.LogWarning("Valid only for 'Only Children'");
			}
		}

		public void Transit(string name)
		{
			Transit_Internal(name, false);
		}

		public void TransitDirectly(string name)
		{
			Transit_Internal(name, true);
		}

		public void TransitSequently(string name)
		{
			if (_transitions == null || _transitions.Count == 0)
			{
				Debug.LogWarning("No transitions");
				return;
			}

			if (name.Equals(_currentStateName))
				return;

			_currentStateName = name;

			if(_sequence.IsActive()) _sequence.Kill();

			// Transit
			UITransition transition = _transitionDic[name];
			List<UIState> states = transition.states;
			Sequence sequence = DOTween.Sequence();

			transition.onStart?.Invoke();
			for (int i = 0; i < states.Count; i++)
			{
				Tween tween = states[i].Transit();
				if (tween != null) sequence.Append(tween);
			}
			sequence.onComplete += () => transition.onFinished?.Invoke();

			_sequence = sequence;
		}

		private void Transit_Internal(string name, bool directly)
		{
			if (_transitions == null || _transitions.Count == 0)
			{
				Debug.LogWarning("No transitions");
				return;
			}

			if (name.Equals(_currentStateName))
				return;

			_currentStateName = name;

			if (_sequence.IsActive()) _sequence.Kill();

			UITransition transition = _transitionDic[name];
			List<UIState> states = transition.states;
			Sequence sequence = DOTween.Sequence();

			transition.onStart?.Invoke();
			for (int i = 0; i < states.Count; i++)
			{
				Tween tween = states[i].Transit(directly);
				if (tween != null) sequence.Join(tween);
			}
			sequence.onComplete += () => transition.onFinished?.Invoke();

			_sequence = sequence;
		}
	}
}
