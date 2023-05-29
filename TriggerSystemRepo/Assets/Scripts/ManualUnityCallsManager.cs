using System;
using System.Collections.Generic;
using UnityEngine;

namespace SugoiSenshuFactory.ManualUnityCalls
{
	public class ManualUnityCallsManager : MonoBehaviour
	{
		[SerializeField]
		private SegregatedManualUnityCallsMonoBehaviour[] awakeManualUnityCallsCollection;
		[SerializeField]
		private SegregatedManualUnityCallsMonoBehaviour[] startManualUnityCallsCollection;
		[SerializeField]
		private List<SegregatedManualUnityCallsMonoBehaviour> updateManualUnityCallsCollection = new();
		[SerializeField]
		private List<SegregatedManualUnityCallsMonoBehaviour> lateUpdateManualUnityCallsCollection = new();
		[SerializeField]
		private List<SegregatedManualUnityCallsMonoBehaviour> fixedUpdateManualUnityCallsCollection = new();

		public void RegisterManualUnityCallsMonoBehaviourForUpdate (ManualUnityCallsMonoBehaviour newManualUnityCallsMonoBehaviour, int callOrder)
		{
			AddAndSortSegregatedManualUnityCallsMonoBehaviour(updateManualUnityCallsCollection, newManualUnityCallsMonoBehaviour, callOrder);
		}

		public void RegisterManualUnityCallsMonoBehaviourForLateUpdate (ManualUnityCallsMonoBehaviour newManualUnityCallsMonoBehaviour, int callOrder)
		{
			AddAndSortSegregatedManualUnityCallsMonoBehaviour(lateUpdateManualUnityCallsCollection, newManualUnityCallsMonoBehaviour, callOrder);
		}

		public void RegisterManualUnityCallsMonoBehaviourForFixedUpdate (ManualUnityCallsMonoBehaviour newManualUnityCallsMonoBehaviour, int callOrder)
		{
			AddAndSortSegregatedManualUnityCallsMonoBehaviour(fixedUpdateManualUnityCallsCollection, newManualUnityCallsMonoBehaviour, callOrder);
		}

		protected virtual void Awake ()
		{
			for (int i = 0; i < awakeManualUnityCallsCollection.Length; i++)
			{
				awakeManualUnityCallsCollection[i].BoundManualUnityCallsMonoBehaviour.ManualAwake();
			}
		}

		protected virtual void Start ()
		{
			for (int i = 0; i < startManualUnityCallsCollection.Length; i++)
			{
				startManualUnityCallsCollection[i].BoundManualUnityCallsMonoBehaviour.ManualStart();
			}
		}

		protected virtual void Update ()
		{
			for (int i = 0; i < updateManualUnityCallsCollection.Count; i++)
			{
				updateManualUnityCallsCollection[i].BoundManualUnityCallsMonoBehaviour.ManualUpdate();
			}
		}

		protected virtual void LateUpdate ()
		{
			for (int i = 0; i < lateUpdateManualUnityCallsCollection.Count; i++)
			{
				lateUpdateManualUnityCallsCollection[i].BoundManualUnityCallsMonoBehaviour.ManualLateUpdate();
			}
		}

		protected virtual void FixedUpdate ()
		{
			for (int i = 0; i < fixedUpdateManualUnityCallsCollection.Count; i++)
			{
				fixedUpdateManualUnityCallsCollection[i].BoundManualUnityCallsMonoBehaviour.ManualFixedUpdate();
			}
		}
		
		private void AddAndSortSegregatedManualUnityCallsMonoBehaviour (List<SegregatedManualUnityCallsMonoBehaviour> source, ManualUnityCallsMonoBehaviour newManualUnityCallsMonoBehaviour, int callOrder)
		{
			SegregatedManualUnityCallsMonoBehaviour newSegregatedManualUnityCallsMonoBehaviour = new (newManualUnityCallsMonoBehaviour, callOrder);
			source.Add(newSegregatedManualUnityCallsMonoBehaviour);
			source.Sort(SegregatedManualUnityCallsMonoBehaviour.CALL_ORDER_ASCENDING_COMPARER);
		}

		[System.Serializable]
		public class SegregatedManualUnityCallsMonoBehaviour
		{
			public SegregatedManualUnityCallsMonoBehaviour (ManualUnityCallsMonoBehaviour manualUnityCallsMonoBehaviour, int callOrder)
			{
				BoundManualUnityCallsMonoBehaviour = manualUnityCallsMonoBehaviour;
				CallOrder = callOrder;
			}

			[field: SerializeField]
			public ManualUnityCallsMonoBehaviour BoundManualUnityCallsMonoBehaviour { get; private set; }
			[field: SerializeField]
			public int CallOrder { get; private set; }
			
			public static readonly Comparer<SegregatedManualUnityCallsMonoBehaviour> CALL_ORDER_ASCENDING_COMPARER = Comparer<SegregatedManualUnityCallsMonoBehaviour>.Create((x, y) => x.CallOrder.CompareTo(y.CallOrder));
		}

	#if UNITY_EDITOR
		[ContextMenu(nameof(SortManualUnityCallsCollections))]
		private void SortManualUnityCallsCollections ()
		{
			Array.Sort(awakeManualUnityCallsCollection, SegregatedManualUnityCallsMonoBehaviour.CALL_ORDER_ASCENDING_COMPARER);
			Array.Sort(startManualUnityCallsCollection, SegregatedManualUnityCallsMonoBehaviour.CALL_ORDER_ASCENDING_COMPARER);
			updateManualUnityCallsCollection.Sort(SegregatedManualUnityCallsMonoBehaviour.CALL_ORDER_ASCENDING_COMPARER);
			lateUpdateManualUnityCallsCollection.Sort(SegregatedManualUnityCallsMonoBehaviour.CALL_ORDER_ASCENDING_COMPARER);
			fixedUpdateManualUnityCallsCollection.Sort(SegregatedManualUnityCallsMonoBehaviour.CALL_ORDER_ASCENDING_COMPARER);
		}

		[ContextMenu(nameof(RemoveClassThatHasNoDeclaredOverride))]
		private void RemoveClassThatHasNoDeclaredOverride ()
		{
			TryToRemoveNotOverridenElements(ref awakeManualUnityCallsCollection, nameof(ManualUnityCallsMonoBehaviour.ManualAwake));
			TryToRemoveNotOverridenElements(ref startManualUnityCallsCollection, nameof(ManualUnityCallsMonoBehaviour.ManualStart));
			TryToRemoveNotOverridenElements(updateManualUnityCallsCollection, nameof(ManualUnityCallsMonoBehaviour.ManualUpdate));
			TryToRemoveNotOverridenElements(lateUpdateManualUnityCallsCollection, nameof(ManualUnityCallsMonoBehaviour.ManualLateUpdate));
			TryToRemoveNotOverridenElements(fixedUpdateManualUnityCallsCollection, nameof(ManualUnityCallsMonoBehaviour.ManualFixedUpdate));
		}

		private void TryToRemoveNotOverridenElements (List<SegregatedManualUnityCallsMonoBehaviour> source, string compareMethodName)
		{
			for (int i = source.Count - 1; i >= 0; i--)
			{
				if (IsMissingOverride(source[i], compareMethodName) == true)
				{
					source.Remove(source[i]);
				}
			}
		}

		private void TryToRemoveNotOverridenElements (ref SegregatedManualUnityCallsMonoBehaviour[] source, string compareMethodName)
		{
			List<SegregatedManualUnityCallsMonoBehaviour> elementsToRemove = new(source);

			for (int i = source.Length - 1; i >= 0; i--)
			{
				if (IsMissingOverride(source[i], compareMethodName) == true)
				{
					elementsToRemove.Remove(source[i]);
				}
			}

			source = elementsToRemove.ToArray();
		}

		private bool IsMissingOverride (SegregatedManualUnityCallsMonoBehaviour source, string compareMethodName)
		{
			return source.BoundManualUnityCallsMonoBehaviour.GetType().GetMethod(compareMethodName).DeclaringType == typeof(ManualUnityCallsMonoBehaviour);
		}
	#endif
	}
}
