using UnityEngine;

public class Load : MonoBehaviour
{
    public string containerColor;
    public AudioClip[] hitSounds;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 2f)
        {
            if (hitSounds.Length > 0)
            {
                int index = Random.Range(0, hitSounds.Length);
                AudioClip selected = hitSounds[index];
                AudioManagerVR.Instance.PlaySFX2D(selected);
            }
        }
    }
}
