using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraManager : MonoBehaviour
{
    //based off of Raycastly on youtube
    static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
    public static CinemachineVirtualCamera activeCamera = null;

    public static bool IsActiveCamera(CinemachineVirtualCamera camera)
    {
        return camera == activeCamera;
    }
    public static void SwitchCamera(CinemachineVirtualCamera newcamera)
    {
        newcamera.Priority = 10;
        activeCamera = newcamera;

        foreach (CinemachineVirtualCamera cam in cameras)
        {
            if (cam != newcamera)
            {
                cam.Priority = 0;
            }
        }
    }
    //method to register and unregister cameras
    public static void Register(CinemachineVirtualCamera camera)
    {
        cameras.Add(camera);
    }
    public static void Unregister(CinemachineVirtualCamera camera)
    {
        cameras.Remove(camera);
    }
}
