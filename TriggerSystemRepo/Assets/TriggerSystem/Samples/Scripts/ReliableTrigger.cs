using System;
using UnityEngine;

namespace SugoiSenshuFactory.TriggerSystem
{
	public class ReliableTrigger : MonoBehaviour, IReliableTrigger
	{
		public event Action OnReliableTriggerDisableOrDestroy = delegate { };

		private void OnDisable ()
		{
			OnReliableTriggerDisableOrDestroy.Invoke();
		}

		private void OnDestroy ()
		{
			OnReliableTriggerDisableOrDestroy.Invoke();
		}
	}
}

