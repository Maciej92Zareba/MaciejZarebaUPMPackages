using System.ComponentModel;
using UnityEngine;

namespace SugoiSenshuFactory.ManualUnityCalls
{
    public class ManualUnityCallsMonoBehaviour : MonoBehaviour
    {
        [field: SerializeField, ReadOnly(true)] 
        public uint CallOrder { get; protected set; } = 0;

        public virtual void ManualAwake()
        {
        
        }

        public virtual void ManualStart()
        {
        
        }

        public virtual void ManualUpdate()
        {
        
        }

        public virtual void ManualLateUpdate()
        {
        
        }

        public virtual void ManualFixedUpdate()
        {
        
        }
    }
}
