using System;
using System.Collections.Generic;
using UnityEngine;

namespace SugoiSenshuFactory.TriggerSystem
{
    public class TriggerActionBroadcaster : MonoBehaviour
    {
        [SerializeField] 
        private List<BaseTriggerAction> triggerActionsCollection = new();
        [SerializeField] 
        private List<TriggerAnalyzer> triggerAnalyzersCollection = new();

        private Action<Collider> cachedAnalyzeTriggersActivationState;
        private bool lastState = false;

        protected virtual void Awake()
        {
            cachedAnalyzeTriggersActivationState = AnalyzeTriggersActivationState;
        }

        protected virtual void OnEnable()
        {
            AttachToEvents();
        }

        protected virtual void OnDisable()
        {
            DetachFromEvents();
        }

        protected virtual bool GetCurrentState()
        {
            bool currentState = false;

            for (int i = 0; i < triggerAnalyzersCollection.Count; i++)
            {
                if (triggerAnalyzersCollection[i].IsBusy == true)
                {
                    currentState = true;
                }
            }

            return currentState;
        }

        protected virtual void AttachToEvents()
        {
            for (int i = 0; i < triggerAnalyzersCollection.Count; i++)
            {
                triggerAnalyzersCollection[i].OnTriggerStateChange += cachedAnalyzeTriggersActivationState;
            }
        }

        protected virtual void DetachFromEvents()
        {
            for (int i = 0; i < triggerAnalyzersCollection.Count; i++)
            {
                triggerAnalyzersCollection[i].OnTriggerStateChange -= cachedAnalyzeTriggersActivationState;
            }
        }

        private void AnalyzeTriggersActivationState(Collider stateChangingCollider)
        {
            bool currentState = GetCurrentState();

            if (currentState != lastState)
            {
                if (currentState == true)
                {
                    BroadcastTriggerEnter(stateChangingCollider);
                }
                else
                {
                    BroadcastTriggerExit(stateChangingCollider);
                }

                lastState = currentState;
            }
        }

        private void BroadcastTriggerEnter(Collider enteringTriggerCollider)
        {
            for (int i = 0; i < triggerActionsCollection.Count; i++)
            {
                triggerActionsCollection[i].StartTriggerEnterAction(enteringTriggerCollider);
            }
        }

        private void BroadcastTriggerExit(Collider exitingTriggerCollider)
        {
            for (int i = 0; i < triggerActionsCollection.Count; i++)
            {
                triggerActionsCollection[i].StartTriggerExitAction(exitingTriggerCollider);
            }
        }
        
        #if UNITY_EDITOR
        [ContextMenu(nameof(CollectTriggerActions))]
        private void CollectTriggerActions()
        {
            GetComponentsInChildren(triggerActionsCollection);
        }

        [ContextMenu(nameof(CollectTriggerAnalyzers))]
        private void CollectTriggerAnalyzers()
        {
            GetComponentsInChildren(triggerAnalyzersCollection);
        }
        #endif
    }
}