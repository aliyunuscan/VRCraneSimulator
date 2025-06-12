using UnityEngine;

public class Rope : MonoBehaviour
{
    [Header("Rope Points")]
    public Transform basePoint;
    public Rigidbody hook;

    [Header("Rope Settings")]
    public float maxLength = 5f;
    public float stiffness = 300f;
    public float damping = 25f;

    [Header("Visual")]
    public LineRenderer ropeRenderer;

    void Start()
    {
        if (ropeRenderer != null)
        {
            ropeRenderer.positionCount = 2;
        }
    }

    void FixedUpdate()
    {
        if (hook == null || basePoint == null)
            return;

        Vector3 offset = hook.position - basePoint.position;
        float length = offset.magnitude;
        if (length > maxLength)
        {
            Vector3 dir = offset / length;
            float stretch = length - maxLength;

            Vector3 force = -dir * (stiffness * stretch);
            Vector3 velAlongRope = Vector3.Project(hook.linearVelocity, dir);
            Vector3 damp = -velAlongRope * damping;

            hook.AddForce(force + damp);
        }

        if (ropeRenderer != null)
        {
            ropeRenderer.SetPosition(0, basePoint.position);
            ropeRenderer.SetPosition(1, hook.position);
        }
    }
}
