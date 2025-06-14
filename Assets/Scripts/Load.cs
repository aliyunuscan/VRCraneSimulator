using UnityEngine;
using System;

public class Load : MonoBehaviour
{
    public event Action OnHooked;
    public event Action OnUnhooked;

    public bool IsHooked { get; private set;}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Hook"))
        {
            if (!IsHooked)
            {
                IsHooked = true;
                OnHooked?.Invoke();
                Debug.Log("Load hooked!");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Hook"))
        {
            if (IsHooked)
            {
                IsHooked = false;
                OnUnhooked?.Invoke();
                Debug.Log("Load unhooked!");
            }
        }
    }

}
