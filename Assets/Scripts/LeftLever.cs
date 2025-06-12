// LeftLever.cs
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LeftLever : MonoBehaviour
{
    [Header("CraneController Reference")]
    public CraneController CraneController;

    [Header("InputAction References")]
    private InputAction leftThumbstickAction;
    private XRGrabInteractable grabInteractable;
    private InputManager inputManager;
    [Header("Restrictor")]
    private PlayerRestrictor playerRestrictor;
    [Header("Other Variables")]
    private float deadzone = 0.1f;
    private Coroutine holdCoroutine = null;
    private bool isGrabbed = false;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        playerRestrictor = FindAnyObjectByType<PlayerRestrictor>();

        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void Start()
    {
        inputManager = GetComponentInParent<InputManager>();
        leftThumbstickAction = inputManager.Actions.XRILeft.Thumbstick;
        leftThumbstickAction.performed += OnThumbstickChanged;
        leftThumbstickAction.canceled += OnThumbstickChanged;

        leftThumbstickAction.Enable();

        leftThumbstickAction.Disable();
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);

        leftThumbstickAction.performed -= OnThumbstickChanged;
        leftThumbstickAction.canceled -= OnThumbstickChanged;
    }

    private void OnEnable()
    {
        if (leftThumbstickAction == null) return;
        leftThumbstickAction.performed += OnThumbstickChanged;
        leftThumbstickAction.canceled += OnThumbstickChanged;

        leftThumbstickAction.Enable();
    }

    private void OnDisable()
    {
        leftThumbstickAction.performed -= OnThumbstickChanged;
        leftThumbstickAction.canceled -= OnThumbstickChanged;
        leftThumbstickAction.Disable();
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        isGrabbed = true;
        leftThumbstickAction.Enable();

        playerRestrictor.DisableLocomotion();
        playerRestrictor.DisableTeleportInteractors();
        //playerRestrictor.DisableNearFarInteractor();
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        isGrabbed = false;

        if (holdCoroutine != null)
        {
            StopCoroutine(holdCoroutine);
            holdCoroutine = null;
        }

        leftThumbstickAction?.Disable();

        playerRestrictor.EnableLocomotion();
        playerRestrictor.EnableTeleportInteractors();
        //playerRestrictor.EnableNearFarInteractor();
    }

    private void OnThumbstickChanged(InputAction.CallbackContext context)
    {

        Vector2 axis = context.ReadValue<Vector2>();
        Debug.Log($"[LeftLever] Axis: X={axis.x:F2}, Y={axis.y:F2}");

        bool moveCar = Mathf.Abs(axis.y) >= Mathf.Abs(axis.x);
        float value = moveCar ? axis.y : axis.x;

        if (Mathf.Abs(value) <= deadzone)
        {
            if (holdCoroutine != null)
            {
                StopCoroutine(holdCoroutine);
                holdCoroutine = null;
            }
            return;
        }

        if (holdCoroutine == null)
        {
            holdCoroutine = moveCar
                ? StartCoroutine(CarMoveRoutine())
                : StartCoroutine(BoomRotateRoutine());
        }
    }

    private IEnumerator CarMoveRoutine()
    {
        while (true)
        {
            if (!isGrabbed)
            {
                holdCoroutine = null;
                yield break;
            }

            float currentY = leftThumbstickAction.ReadValue<Vector2>().y;
            if (Mathf.Abs(currentY) <= deadzone)
            {
                holdCoroutine = null;
                yield break;
            }

            CraneController.MoveCar(currentY);
            Debug.Log($"[LeftLever] (Hold) Car Move Y: {currentY:F2}");
            yield return null;
        }
    }

    private IEnumerator BoomRotateRoutine()
    {
        while (true)
        {
            if (!isGrabbed)
            {
                holdCoroutine = null;
                yield break;
            }

            float currentX = leftThumbstickAction.ReadValue<Vector2>().x;
            if (Mathf.Abs(currentX) <= deadzone)
            {
                holdCoroutine = null;
                yield break;
            }

            CraneController.CraneRotation(currentX);
            Debug.Log($"[LeftLever] (Hold) Boom Rotate X: {currentX:F2}");
            yield return null;
        }
    }
}
