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
    private Rope rope;
    public Transform craneRotationPivot;
    public float rotationSpeed=100f;
    public float rotationSmoothness = 5f;

    void Start()
    {
        rope = FindAnyObjectByType<Rope>();
    }

    public void MoveHookY(float input)
    {
        if (hook == null)
        {
            Debug.LogWarning("Hook reference is missing!");
            return;
        }

        float delta = input * hookSpeed * Time.deltaTime;

        if (rope.maxLength < 23.0f && rope.maxLength >= 0.0f) rope.maxLength += delta;
        else if ((23.0f - rope.maxLength) < rope.maxLength) rope.maxLength = 22.9f;
        else rope.maxLength = 0.0f;

    }


    public void MoveCar(float input)
    {
        if (car == null)
        {
            Debug.Log("Car could not found!");
            return;
        }

        float delta = -input * carSpeed * Time.deltaTime;
        Vector3 localPos = car.localPosition;

        float minX = Mathf.Min(boomStartCorner.localPosition.x, boomEndCorner.localPosition.x);
        float maxX = Mathf.Max(boomStartCorner.localPosition.x, boomEndCorner.localPosition.x);

        float newX = Mathf.Clamp(localPos.x + delta, minX, maxX);

        car.localPosition = new Vector3(newX, localPos.y, localPos.z);

        Vector3 targetHookPos = new Vector3(car.localPosition.x, hook.localPosition.y, hook.localPosition.z);
        //hook.localPosition = Vector3.Lerp(hook.localPosition, targetHookPos, hookFollowSpeed * Time.deltaTime);

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

        currentInput = Mathf.Lerp(currentInput, input, Time.deltaTime * rotationSmoothness);
        float rotationAmount = currentInput * rotationSpeed * Time.deltaTime;
        craneRotationPivot.Rotate(0f, rotationAmount, 0f);
    }
}
