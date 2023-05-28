using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SugoiSenshuFactory.ManualUnityCalls
{
	public class ManualUnityCallsManager : MonoBehaviour
	{
		[SerializeField]
		private ManualUnityCallsMonoBehaviour[] awakeManualUnityCallsCollection;
		[SerializeField]
		private ManualUnityCallsMonoBehaviour[] startManualUnityCallsCollection;
		[SerializeField]
		private List<ManualUnityCallsMonoBehaviour> updateManualUnityCallsCollection = new();
		[SerializeField]
		private List<ManualUnityCallsMonoBehaviour> lateUpdateManualUnityCallsCollection = new();
		[SerializeField]
		private List<ManualUnityCallsMonoBehaviour> fixedUpdateManualUnityCallsCollection = new();

		protected virtual void Awake ()
		{
			for (int i = 0; i < awakeManualUnityCallsCollection.Length; i++)
			{
				awakeManualUnityCallsCollection[i].ManualAwake();
			}
		}

		protected virtual void Start ()
		{
			for (int i = 0; i < startManualUnityCallsCollection.Length; i++)
			{
				startManualUnityCallsCollection[i].ManualStart();
			}
		}

		protected virtual void Update ()
		{
			for (int i = 0; i < updateManualUnityCallsCollection.Count; i++)
			{
				updateManualUnityCallsCollection[i].ManualUpdate();
			}
		}

		protected virtual void LateUpdate ()
		{
			for (int i = 0; i < lateUpdateManualUnityCallsCollection.Count; i++)
			{
				lateUpdateManualUnityCallsCollection[i].ManualLateUpdate();
			}
		}

		protected virtual void FixedUpdate ()
		{
			for (int i = 0; i < fixedUpdateManualUnityCallsCollection.Count; i++)
			{
				fixedUpdateManualUnityCallsCollection[i].ManualFixedUpdate();
			}
		}

	#if UNITY_EDITOR
		[ContextMenu(nameof(SortManualUnityCallsCollections))]
		private void SortManualUnityCallsCollections ()
		{
			awakeManualUnityCallsCollection = awakeManualUnityCallsCollection.OrderBy(o => o.CallOrder).ToArray();
			startManualUnityCallsCollection = startManualUnityCallsCollection.OrderBy(o => o.CallOrder).ToArray();
			updateManualUnityCallsCollection = updateManualUnityCallsCollection.OrderBy(o => o.CallOrder).ToList();
			lateUpdateManualUnityCallsCollection = lateUpdateManualUnityCallsCollection.OrderBy(o => o.CallOrder).ToList();
			fixedUpdateManualUnityCallsCollection = fixedUpdateManualUnityCallsCollection.OrderBy(o => o.CallOrder).ToList();
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

		private void TryToRemoveNotOverridenElements<T> (List<T> source, string compareMethodName)
		{
			for (int i = source.Count - 1; i >= 0; i--)
			{
				if (IsMissingOverride(source[i], compareMethodName) == true)
				{
					source.Remove(source[i]);
				}
			}
		}

		private void TryToRemoveNotOverridenElements<T> (ref T[] source, string compareMethodName)
		{
			List<T> elementsToRemove = new(source);

			for (int i = source.Length - 1; i >= 0; i--)
			{
				if (IsMissingOverride(source[i], compareMethodName) == true)
				{
					elementsToRemove.Remove(source[i]);
				}
			}

			source = elementsToRemove.ToArray();
		}

		private bool IsMissingOverride<T> (T source, string compareMethodName)
		{
			return source.GetType().GetMethod(compareMethodName).DeclaringType == typeof(ManualUnityCallsMonoBehaviour);
		}
	#endif
	}
}
