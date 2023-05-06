using UnityEngine;

namespace SugoiSenshuFactory.TriggerSystem
{
	public abstract class BaseTriggerAction : MonoBehaviour
	{
		[SerializeField]
		private bool activeTriggerOnlyOnce = true;

		[SerializeField] 
		private bool stopActionOnTriggerExit = true;

		private bool currentTriggerState;
		private bool wasTriggered;
		private bool canExecuteExitAction;

		public void StartTriggerEnterAction (Collider enteringTriggerCollider)
		{
			if (CanExecuteTriggerEnterAction() == true)
			{
				TriggerEnterAction(enteringTriggerCollider);
			}
		}

		public void StartTriggerExitAction (Collider exitingTriggerCollider)
		{
			if (CanExecuteTriggerExitAction() == true)
			{
				currentTriggerState = false;
				TriggerExitAction(exitingTriggerCollider);
			}
		}

		protected abstract void TriggerEnterAction (Collider enteringTriggerCollider);
		protected abstract void TriggerExitAction (Collider exitingTriggerCollider);

		private bool CanExecuteTriggerEnterAction ()
		{
			currentTriggerState = GetCurrentTriggerStateBasedOnWasTriggered();
			wasTriggered = true;

			return currentTriggerState;
		}

		private bool GetCurrentTriggerStateBasedOnWasTriggered ()
		{
			return (activeTriggerOnlyOnce != true || wasTriggered != true);
		}

		private bool CanExecuteTriggerExitAction ()
		{
			canExecuteExitAction = (stopActionOnTriggerExit == true && currentTriggerState == true);
			return canExecuteExitAction;
		}
	}
}
