using UnityEngine;

namespace SugoiSenshuFactory.ManualUnityCalls
{
	public class ExampleRegisterElement : ManualUnityCallsMonoBehaviour
	{
		private ManualUnityCallsManager boundManualUnityCallsManager;
		
		public void RegisterToManualUpdateWithRandomOrder (ManualUnityCallsManager manager)
		{
			boundManualUnityCallsManager = manager;
			boundManualUnityCallsManager.RegisterManualUnityCallsMonoBehaviourForUpdate(this,Random.Range(0, 10));
		}

		private void OnDestroy () 
		{
			boundManualUnityCallsManager.UnregisterManualUnityCallsMonoBehaviourForUpdate(this);
		}
	}
}
