using System.Collections.Generic;
using UnityEngine;

namespace SugoiSenshuFactory.TriggerSystem
{
	public class SetActiveTriggerAction : BaseTriggerAction
	{
		[SerializeField]
		private List<GameObject> objectsToSetActiveCollection = new ();
		[SerializeField]
		private bool setActiveStateOnEnter = false;

		protected override void TriggerEnterAction (Collider enteringTriggerCollider)
		{
			SetActiveState(setActiveStateOnEnter);
		}

		protected override void TriggerExitAction (Collider exitingTriggerCollider)
		{
			SetActiveState(setActiveStateOnEnter == false);
		}

		private void SetActiveState (bool state)
		{
			for (int i = 0; i < objectsToSetActiveCollection.Count; i++)
			{
				objectsToSetActiveCollection[i].SetActive(state);
			}
		}
	}
}
