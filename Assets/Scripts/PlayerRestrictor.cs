using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PlayerRestrictor : MonoBehaviour
{
    [SerializeField] private GameObject Locomotion;
    [SerializeField] private GameObject Turn; // In Locomotion
    [SerializeField] private GameObject Move; // In Locomotion   
    [SerializeField] private GameObject Teleportation; // In Locomotion
    [SerializeField] private GameObject RightTeleportInteractor;
    [SerializeField] private GameObject RightNearFarInteractor;
    [SerializeField] private GameObject RightTeleportStabilizer;
    [SerializeField] private GameObject LeftTeleportInteractor;
    [SerializeField] private GameObject LeftNearFarInteractor;
    [SerializeField] private GameObject LeftTeleportStabilizer;

    public void DisableLocomotion()
    {
        Locomotion.SetActive(false);
    }
    public void EnableLocomotion()
    {
        Locomotion.SetActive(true);
    }
    public void EnableNearFarInteractor()
    {
        RightNearFarInteractor.SetActive(true);
        LeftNearFarInteractor.SetActive(true);
    }
    public void DisableNearFarInteractor()
    {
        RightNearFarInteractor.SetActive(false);
        LeftNearFarInteractor.SetActive(false);
    }

    public void DisableTeleportInteractors()
    {
        RightTeleportInteractor.SetActive(false);
        RightTeleportStabilizer.SetActive(false);
        LeftTeleportInteractor.SetActive(false);
        LeftTeleportStabilizer.SetActive(false);
    }
    public void EnableTeleportInteractors()
    {
        RightTeleportInteractor.SetActive(true);
        RightTeleportStabilizer.SetActive(true);
        LeftTeleportInteractor.SetActive(true);
        LeftTeleportStabilizer.SetActive(true);
    }
}
