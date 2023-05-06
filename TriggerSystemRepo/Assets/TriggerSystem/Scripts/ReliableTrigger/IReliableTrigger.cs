using System;
using UnityEngine;

namespace SugoiSenshuFactory.TriggerSystem
{
    public interface IReliableTrigger
    {
        public Collider BoundTriggerCollider { get; }
        public event Action<Collider> OnReliableTriggerDisableOrDestroy;
    }
}