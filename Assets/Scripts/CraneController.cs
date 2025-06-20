// CraneController.cs
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CraneController : MonoBehaviour
{
    public Transform hook;
    public float hookSpeed = 1f;
    [SerializeField] private float hookFollowSpeed = 5f;
    public Transform car;
    public float carSpeed = 5f;
    public Transform boomStartCorner;
    public Transform boomEndCorner;
    public Rope hookRope;
    public Transform craneRotationPivot;
    public float rotationSpeed = 100f;
    public float rotationSmoothness = 5f;

    public GameObject dangerPanel;
    private bool isDangerPanelOpen = false;

    public AudioClip DangerPanelAudioClip;

    public void MoveHookY(float input)
    {
        if (hookRope == null)
        {
            Debug.LogWarning("Hook reference is missing!");
            return;
        }

        DangerPanelCheck(input);

        float delta = input * hookSpeed * Time.deltaTime;
        hookRope.maxLength += delta;

        hookRope.maxLength = Mathf.Clamp(hookRope.maxLength, 0.0f, 23.0f);
    }


    public void MoveCar(float input)
    {
        if (car == null)
        {
            Debug.Log("Car could not found!");
            return;
        }

        DangerPanelCheck(input);

        float delta = -input * carSpeed * Time.deltaTime;
        Vector3 localPos = car.localPosition;

        float minX = Mathf.Min(boomStartCorner.localPosition.x, boomEndCorner.localPosition.x);
        float maxX = Mathf.Max(boomStartCorner.localPosition.x, boomEndCorner.localPosition.x);

        float newX = Mathf.Clamp(localPos.x + delta, minX, maxX);

        car.localPosition = new Vector3(newX, localPos.y, localPos.z);

        Vector3 targetHookPos = new Vector3(car.localPosition.x, hook.localPosition.y, hook.localPosition.z);

        Debug.Log($"Moving car: Input={input} Delta={delta} NewX={newX}");
    }

    public void CraneRotation(float input)
    {
        if (craneRotationPivot == null)
        {
            Debug.Log("CraneRotationPivot could not found!");
            return;
        }

        float currentInput = 0f;

        DangerPanelCheck(input);

        currentInput = Mathf.Lerp(currentInput, input, Time.deltaTime * rotationSmoothness);
        float rotationAmount = currentInput * rotationSpeed * Time.deltaTime;
        craneRotationPivot.Rotate(0f, rotationAmount, 0f);
    }

    private void DangerPanelCheck(float input)
    {
        bool shouldShow = Mathf.Abs(input) >= 0.8f;
        if (shouldShow != isDangerPanelOpen)
        {
            AudioManagerVR.Instance.PlaySFX2D(DangerPanelAudioClip);
            dangerPanel.SetActive(shouldShow);
            isDangerPanelOpen = shouldShow;
        }
    }



}
