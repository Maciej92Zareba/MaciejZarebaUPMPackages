using UnityEngine;

namespace SugoiSenshuFactory.TriggerSystem
{
    public class TriggerAnalyzerVisualization : MonoBehaviour
    {
        [SerializeField] private Material triggerBusyMaterial = null;
        [SerializeField] private Material triggerNotBusyMaterial = null;
        [SerializeField] private MeshRenderer boundMeshRenderer = null;

        private Material boundTriggerBusyMaterialInstance;
        private Material boundTriggerNotBusyMaterialInstance;

        protected virtual void Awake()
        {
            PrepareMaterialsInstances();
            boundMeshRenderer.sharedMaterial = boundTriggerNotBusyMaterialInstance;
        }

        protected void OnTriggerEnter(Collider other)
        {
            IReliableTrigger reliableTrigger = other.GetComponent<IReliableTrigger>();

            if (reliableTrigger != null)
            {
                boundMeshRenderer.sharedMaterial = boundTriggerBusyMaterialInstance;
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            IReliableTrigger reliableTrigger = other.GetComponent<IReliableTrigger>();
            
            if (reliableTrigger != null)
            {
                boundMeshRenderer.sharedMaterial = boundTriggerNotBusyMaterialInstance;
            }
        }

        private void PrepareMaterialsInstances()
        {
            boundTriggerBusyMaterialInstance = new Material(triggerBusyMaterial);
            boundTriggerNotBusyMaterialInstance = new Material(triggerNotBusyMaterial);
        }
    }
}