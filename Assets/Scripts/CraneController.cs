// CraneController.cs
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CraneController : MonoBehaviour
{
    public Transform hook;
    public float hookSpeed = 2f;
    public float ropeLength = 5f;
    // public float hookMinY = 0f;
    // public float hookMaxY = 5f;
    public float carYSize;
    [SerializeField] private float hookFollowSpeed = 5f;
    public Transform car;
    public float carSpeed = 5f;
    public Transform boomStartCorner;
    public Transform boomEndCorner;
    public SpringJoint hookSJ;
    public Transform craneRotationPivot;
    public float rotationSpeed=100f;
    public float rotationSmoothness = 5f;

    public void MoveHookY(float input)
    {
        if (hook == null)
        {
            Debug.LogWarning("Hook reference is missing!");
            return;
        }

        float delta = input * hookSpeed * Time.deltaTime;

        if (hookSJ.maxDistance < 15.0f && hookSJ.maxDistance >= 0.0f) hookSJ.maxDistance += delta;
        
        // Vector3 localPos = hook.localPosition;
        // float newY = Mathf.Clamp(localPos.y + delta, car.localPosition.y - ropeLength, car.localPosition.y - carYSize);

        // Debug.Log($"Moving hook: Input={input} Delta={delta} NewY={newY}");

        // hook.localPosition = new Vector3(localPos.x, newY, localPos.z);
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
