using System;
using UnityEngine;

namespace SugoiSenshuFactory.TriggerSystem
{
    public class TriggerAnalyzer : MonoBehaviour
    {
		public event Action OnTriggerStateChange = delegate { };

		public bool IsBusy { get; private set; }

		private IReliableTrigger cachedIReliableTrigger;
		private Action cachedForceTriggerExit;

		protected virtual void Awake()
		{
			cachedForceTriggerExit = ForceTriggerExit;
		}
		
		protected virtual void OnTriggerEnter (Collider other)
		{
			if (IsEnteringReliableTrigger(other) == true)
			{
				cachedIReliableTrigger.OnReliableTriggerDisableOrDestroy += cachedForceTriggerExit;
				IsBusy = true;
				OnTriggerStateChange.Invoke();
			}			
		}

		protected virtual void OnTriggerExit (Collider other)
		{
			if (IsEnteringReliableTrigger(other) == true)
			{
				ForceTriggerExit();
			}
		}

		private void ForceTriggerExit ()
		{
			cachedIReliableTrigger.OnReliableTriggerDisableOrDestroy -= cachedForceTriggerExit;
			IsBusy = false;
			OnTriggerStateChange.Invoke();
		}

		private bool IsEnteringReliableTrigger (Collider other)
		{
			cachedIReliableTrigger = other.GetComponent<IReliableTrigger>();
			return cachedIReliableTrigger != null;
		}
	}
}
