using System;
using UnityEngine;

namespace SugoiSenshuFactory.TriggerSystem
{
    public class TriggerAnalyzer : MonoBehaviour
    {
		public event Action<Collider> OnTriggerStateChange = delegate { };

		public bool IsBusy { get; private set; }

		private IReliableTrigger cachedIReliableTrigger;
		private Action<Collider> cachedForceTriggerExit;

		protected virtual void Awake()
		{
			cachedForceTriggerExit = ForceTriggerExit;
		}
		
		protected virtual void OnTriggerEnter (Collider enteringTriggerCollider)
		{
			if (IsEnteringReliableTrigger(enteringTriggerCollider) == true)
			{
				cachedIReliableTrigger.OnReliableTriggerDisableOrDestroy += cachedForceTriggerExit;
				IsBusy = true;
				OnTriggerStateChange.Invoke(enteringTriggerCollider);
			}			
		}

		protected virtual void OnTriggerExit (Collider exitingTriggerCollider)
		{
			if (IsEnteringReliableTrigger(exitingTriggerCollider) == true)
			{
				ForceTriggerExit(exitingTriggerCollider);
			}
		}

		private void ForceTriggerExit (Collider exitingTriggerCollider)
		{
			cachedIReliableTrigger.OnReliableTriggerDisableOrDestroy -= cachedForceTriggerExit;
			IsBusy = false;
			OnTriggerStateChange.Invoke(exitingTriggerCollider);
		}

		private bool IsEnteringReliableTrigger (Collider other)
		{
			cachedIReliableTrigger = other.GetComponent<IReliableTrigger>();
			return cachedIReliableTrigger != null;
		}
	}
}
