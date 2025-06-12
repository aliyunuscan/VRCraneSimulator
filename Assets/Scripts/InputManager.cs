using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public XRIDefaultInputActions Actions { get; private set; }

    private void Awake()
    {
        Actions = new XRIDefaultInputActions();
    }

    void OnEnable()
    {
        Actions.Enable();
    }

    void OnDisable()
    {
        Actions.Disable(); 
    }

    
}
