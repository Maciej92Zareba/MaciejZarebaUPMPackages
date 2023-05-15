using System;
using UnityEngine;

namespace SugoiSenshuFactory.TriggerSystem
{
    public class ReliableTrigger : MonoBehaviour, IReliableTrigger
    {
        public event Action<Collider> OnReliableTriggerDisableOrDestroy = delegate { };

        [field: SerializeField] 
        public Collider BoundTriggerCollider { get; private set; }

        private void OnDisable()
        {
            OnReliableTriggerDisableOrDestroy.Invoke(BoundTriggerCollider);
        }

        private void OnDestroy()
        {
            OnReliableTriggerDisableOrDestroy.Invoke(BoundTriggerCollider);
        }
    }
}