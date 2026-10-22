using System.Collections.Generic;
using UnityEngine;

namespace TW.Interaction.Inputs
{
	[DisallowMultipleComponent]
	public class InteractInputProvider : InteractProviderBase
	{

		[SerializeField] private InteractProfile _profile;
		[SerializeField] private List<InteractInput> _inputs = new();

		public IReadOnlyList<InteractInput> Inputs => _inputs;

		private void Awake()
		{
			for (int i = 0; i < _inputs.Count; i++)
			{
				InteractInput item = _inputs[i];
				item.Provider = this;
				item.Index = i;
			}
			if (_operator)
			{
				_operator.OnCompleted += OnInteractCompleted;

			}
		}

		private void OnEnable()
		{
			foreach (InteractInput item in _inputs)
			{
				item.Register();
			}
		}

		private void OnDisable()
		{
			foreach (InteractInput item in _inputs)
			{
				item.Unregister();
			}
		}

		public override void Interact(int index)
		{
			if (!_operator || !_canListen || _target == null || index < 0 || index >= _target.Activities.Count)
			{
				return;
			}
			_canListen = false;

			onCompletedChanged.Invoke(false);
			InteractActivityEvent activityEvent = new() { interactable = _target, activity = _target.Activities[index], user = _operator };
			_operator.StartInteract(activityEvent);
		}


	}

}
