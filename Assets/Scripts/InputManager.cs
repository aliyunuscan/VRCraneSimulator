using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static XRIDefaultInputActions Actions { get; private set; }

    private void Awake()
    {
        Actions = new XRIDefaultInputActions();
    }

    private void OnDestroy()
    {
        if (Actions != null)
        {
            Actions.Disable();
            Actions = null;
        }
    }
}
