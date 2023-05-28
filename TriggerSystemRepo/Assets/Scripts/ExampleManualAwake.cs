using SugoiSenshuFactory.ManualUnityCalls;
using UnityEngine;

public class ExampleManualAwake : ManualUnityCallsMonoBehaviour
{
    public override void ManualAwake()
    {
        Debug.Log($"ManualAwake called with order {CallOrder} by gameobject with name {gameObject.name}", gameObject);
    }
}