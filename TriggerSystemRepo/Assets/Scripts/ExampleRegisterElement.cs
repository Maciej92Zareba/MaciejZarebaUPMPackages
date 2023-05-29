using UnityEngine;

namespace SugoiSenshuFactory.ManualUnityCalls
{
	public class ExampleRegisterElement : ManualUnityCallsMonoBehaviour
	{
		public void RegisterToManualUpdateWithRandomOrder (ManualUnityCallsManager manager)
		{
			manager.RegisterManualUnityCallsMonoBehaviourForUpdate(this,Random.Range(0, 10));
		}
	}
}
