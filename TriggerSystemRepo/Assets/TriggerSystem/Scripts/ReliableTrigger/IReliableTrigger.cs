using System;

namespace SugoiSenshuFactory.TriggerSystem
{
    public interface IReliableTrigger
    {
        public event Action OnReliableTriggerDisableOrDestroy;
    }
}