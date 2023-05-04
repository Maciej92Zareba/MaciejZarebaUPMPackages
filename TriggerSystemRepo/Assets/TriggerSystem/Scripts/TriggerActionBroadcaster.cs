using System;
using System.Collections.Generic;
using UnityEngine;

namespace SugoiSenshuFactory.TriggerSystem
{
	public class TriggerActionBroadcaster : MonoBehaviour
	{
		[field: SerializeField]
		protected List<BaseTriggerAction> TriggerActionsCollection { get; set; } = new ();
		[field: SerializeField]
		protected List<TriggerAnalyzer> TriggerAnalyzersCollection { get; set; } = new ();

		private Action cachedAnalyzeTriggersActivationState;
		private bool lastState = false;

		[ContextMenu(nameof(CollectTriggerActions))]
		public void CollectTriggerActions ()
		{
			GetComponentsInChildren(TriggerActionsCollection);
		}

		[ContextMenu(nameof(CollectTriggerAnalyzers))]
		public void CollectTriggerAnalyzers ()
		{
			GetComponentsInChildren(TriggerAnalyzersCollection);
		}

		protected virtual void Awake()
		{
			cachedAnalyzeTriggersActivationState = AnalyzeTriggersActivationState;
		}
		
		protected virtual void OnEnable ()
		{
			AttachToEvents();
		}

		protected virtual void OnDisable ()
		{
			DetachFromEvents();
		}
		
		protected virtual bool GetCurrentState ()
		{
			bool currentState = false;

			for (int i = 0; i < TriggerAnalyzersCollection.Count; i++)
			{
				if (TriggerAnalyzersCollection[i].IsBusy == true)
				{
					currentState = true;
				}
			}

			return currentState;
		}

		protected virtual void AttachToEvents ()
		{
			for (int i = 0; i < TriggerAnalyzersCollection.Count; i++)
			{
				TriggerAnalyzersCollection[i].OnTriggerStateChange += cachedAnalyzeTriggersActivationState;
			}
		}

		protected virtual void DetachFromEvents ()
		{
			for (int i = 0; i < TriggerAnalyzersCollection.Count; i++)
			{
				TriggerAnalyzersCollection[i].OnTriggerStateChange -= cachedAnalyzeTriggersActivationState;
			}
		}
		
		protected void AnalyzeTriggersActivationState ()
		{
			bool currentState = GetCurrentState();

			if (currentState != lastState)
			{
				if (currentState == true)
				{
					BroadcastTriggerEnter();
				}
				else
				{
					BroadcastTriggerExit();
				}

				lastState = currentState;
			}
		}

		private void BroadcastTriggerEnter ()
		{
			for (int i = 0; i < TriggerActionsCollection.Count; i++)
			{
				TriggerActionsCollection[i].StartTriggerEnterAction(null);
			}
		}

		private void BroadcastTriggerExit ()
		{
			for (int i = 0; i < TriggerActionsCollection.Count; i++)
			{
				TriggerActionsCollection[i].StartTriggerExitAction(null);
			}
		}
	}
}
