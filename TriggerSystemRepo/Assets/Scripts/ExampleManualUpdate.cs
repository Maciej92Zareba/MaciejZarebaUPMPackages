using SugoiSenshuFactory.ManualUnityCalls;
using UnityEngine;

public class ExampleManualUpdate : ManualUnityCallsMonoBehaviour
{
    public override void ManualUpdate()
    {
        Debug.Log($"ManualUpdate called with order {CallOrder} by gameobject with name {gameObject.name}", gameObject);
    }
}
