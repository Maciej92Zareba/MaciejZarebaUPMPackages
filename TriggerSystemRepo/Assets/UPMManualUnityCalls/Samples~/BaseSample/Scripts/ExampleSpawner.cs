using UnityEngine;

namespace SugoiSenshuFactory.ManualUnityCalls
{
	public class ExampleSpawner : ManualUnityCallsMonoBehaviour
	{
		[SerializeField]
		private ExampleRegisterElement elementToSpawn;		
		[SerializeField]
		private int numberOfElementsToSpawn = 5;	
		[SerializeField]
		private ManualUnityCallsManager boundManualUnityCallsManager;

		public override void ManualAwake ()
		{
			for (int i = 0; i < numberOfElementsToSpawn; i++)
			{
				Instantiate(elementToSpawn).RegisterToManualUpdateWithRandomOrder(boundManualUnityCallsManager);
			}
		}
	}
}

