using System.Collections.Generic;
using UnityEngine;

namespace SugoiSenshuFactory.TriggerSystem
{
	public class SetActiveTriggerAction : BaseTriggerAction
	{
		[field: SerializeField]
		protected List<GameObject> ObjectsToSetActiveCollection { get; set; } = new ();
		[field: SerializeField]
		protected bool SetActiveStateOnEnter { get; set; } = false;

		protected override void TriggerEnterAction (Collider enteringCollider)
		{
			SetActiveState(SetActiveStateOnEnter);
		}

		protected override void TriggerExitAction (Collider exitingCollider)
		{
			SetActiveState(SetActiveStateOnEnter == false);
		}

		protected virtual void SetActiveState (bool state)
		{
			for (int i = 0; i < ObjectsToSetActiveCollection.Count; i++)
			{
				ObjectsToSetActiveCollection[i].SetActive(state);
			}
		}
	}
}
