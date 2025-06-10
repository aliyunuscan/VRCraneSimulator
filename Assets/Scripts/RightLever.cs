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

    [Header("Right Contr. Thumstick Action")] //XRI Default Input Actions → XRI RightHand → Thumbstick
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
        rightThumbstickAction = InputManager.Actions.XRIRight.Thumbstick;

        rightThumbstickAction.performed += OnThumbstickChanged;
        rightThumbstickAction.canceled += OnThumbstickChanged;

        isGrabbed = true; //Test purpose
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
            //if (!isGrabbed || rightThumbstickAction == null)
            //{
            //    holdCoroutine = null;
            //    yield break;
            //}

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
