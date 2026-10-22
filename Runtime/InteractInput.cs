using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace TW.Interaction.Inputs
{
	[System.Serializable]
	public class InteractInput
	{
		public string name;
		public Background imageIcon;
		public InputActionReference input;

		public InteractInputProvider Provider { get; set; }
		public int Index { get; set; } = 0;

		private void SetInputListen(InputActionReference input, bool isListening = false)
		{
			if (!input)
				return;

			if (isListening)
			{
				input.action.started += ActionStarted;
				input.action.canceled += ActionCanceled;
				input.action.performed += ActionPerformed;
			}
			else
			{
				input.action.started -= ActionStarted;
				input.action.canceled -= ActionCanceled;
				input.action.performed -= ActionPerformed;
			}
		}

		public void Register()
		{
			if (input != null)
			{
				input.asset.Enable();
			}
			SetInputListen(input, true);
		}

		public void Unregister()
		{
			SetInputListen(input, false);

		}

		private void StopPerforming()
		{
			//if (!_performing)
			//{
			//	return;
			//}
			//_performing = false;
			//onPerformingChanged.Invoke(null);
		}

		private void ActionStarted(InputAction.CallbackContext context)
		{
			if (Provider == null || Provider.Target == null)
			{
				return;
			}
			//_performing = true;
			//onPerformingChanged.Invoke(Provider.Target.transform.gameObject);
		}

		private void ActionCanceled(InputAction.CallbackContext context)
		{
			StopPerforming();
		}

		private void ActionPerformed(InputAction.CallbackContext context)
		{
			StopPerforming();
			if (Provider == null)
			{
				return;
			}
			Provider.Interact(Index);

		}
	}

}
