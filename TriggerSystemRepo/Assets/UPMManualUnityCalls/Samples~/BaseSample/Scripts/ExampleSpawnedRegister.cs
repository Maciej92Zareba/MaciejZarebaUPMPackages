using UnityEngine;

namespace SugoiSenshuFactory.ManualUnityCalls
{
	public class ExampleSpawnedRegister : ManualUnityCallsMonoBehaviour
	{
		public override void ManualUpdate()
		{
			Debug.Log($"Registered ManualUpdate called by gameobject with name {gameObject.name}", gameObject);
		}
	}
}

