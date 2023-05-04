using UnityEngine;

namespace SugoiSenshuFactory.TriggerSystem
{
	public abstract class BaseTriggerAction : MonoBehaviour
	{
		[field: SerializeField]
		private bool ActiveTriggerOnlyOnce { get; set; } = true;
		[field: SerializeField]
		private bool StopActionOnTriggerExit { get; set; }

		private bool CurrentTriggerState { get; set; }
		private bool WasTriggered { get; set; }

		private bool CanExecuteExitAction { get; set; }

		public void StartTriggerEnterAction (Collider enteringCollider)
		{
			if (CanExecuteTriggerEnterAction() == true)
			{
				TriggerEnterAction(enteringCollider);
			}
		}

		public void StartTriggerExitAction (Collider exitingCollider)
		{
			if (CanExecuteTriggerExitAction() == true)
			{
				TriggerExitAction(exitingCollider);
			}
		}

		protected abstract void TriggerEnterAction (Collider enteringCollider);
		protected abstract void TriggerExitAction (Collider exitingCollider);

		private bool CanExecuteTriggerEnterAction ()
		{
			CurrentTriggerState = GetCurrentTriggerStateBasedOnWasTriggered();
			WasTriggered = true;

			return CurrentTriggerState;
		}

		private bool GetCurrentTriggerStateBasedOnWasTriggered ()
		{
			return (ActiveTriggerOnlyOnce != true || WasTriggered != true);
		}

		private bool CanExecuteTriggerExitAction ()
		{
			CanExecuteExitAction = (StopActionOnTriggerExit == true && CurrentTriggerState == true);
			CurrentTriggerState = false;

			return CanExecuteExitAction;
		}
	}
}
