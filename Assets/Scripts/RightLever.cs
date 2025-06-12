using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class RightLever : MonoBehaviour
{
    [Header("CraneController Reference")]
    public CraneController CraneController;

    [Header("Right Contr. Thumstick Action")]
    private InputAction rightThumbstickAction;
    private XRGrabInteractable grabInteractable;
    private float deadzone = 0.1f;
    private Coroutine holdCoroutine = null;
    private bool isGrabbed = false;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void  Start()
    {
        rightThumbstickAction = GetComponentInParent<InputManager>().Actions.XRIRight.Thumbstick;
        rightThumbstickAction.performed += OnThumbstickChanged;
        rightThumbstickAction.canceled += OnThumbstickChanged;

        rightThumbstickAction.Enable();

        rightThumbstickAction.Disable();
    }

    private void OnGrabbed(SelectEnterEventArgs enterArgs)
    {
        isGrabbed = true;
        rightThumbstickAction.Enable();
    }
    private void OnReleased(SelectExitEventArgs exitArgs)
    {
        isGrabbed = false;
        if(holdCoroutine != null)
        {
            StopCoroutine(holdCoroutine);
            holdCoroutine = null;
        }

        rightThumbstickAction.Disable();
    }

    private void OnEnable()
    {
        if (rightThumbstickAction == null) return;
        rightThumbstickAction.performed += OnThumbstickChanged;
        rightThumbstickAction.canceled += OnThumbstickChanged;

        rightThumbstickAction.Enable();
    }

    private void OnDisable()
    {
        rightThumbstickAction.performed -= OnThumbstickChanged;
        rightThumbstickAction.canceled -= OnThumbstickChanged;
        rightThumbstickAction.Disable();
    }

    private void OnThumbstickChanged(InputAction.CallbackContext context)
    {
        Vector2 axis = context.ReadValue<Vector2>();

        if (Mathf.Abs(axis.y) <= deadzone)
        {
            if(holdCoroutine != null)
            {
                StopCoroutine(holdCoroutine);
                holdCoroutine = null;
            }
            return;
        }

        if(holdCoroutine == null)
        {
            holdCoroutine = StartCoroutine(HookMoveRoutine());
        }

        //No need to use axis.x for now
    }

    private IEnumerator HookMoveRoutine()
    {
        while (true)
        {
            float currentY = rightThumbstickAction.ReadValue<Vector2>().y;

            if (Mathf.Abs(currentY) <= deadzone)
            {
                holdCoroutine = null;
                yield break;
            }
            CraneController.MoveHookY(currentY);
            Debug.Log($"[RightLever] (Hold) Joystick Y: {currentY:F2}");

            yield return null;//wait till next frame
        }
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);

        rightThumbstickAction.performed -= OnThumbstickChanged;
        rightThumbstickAction.canceled -= OnThumbstickChanged;
    }
}
