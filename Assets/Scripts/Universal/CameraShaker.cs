using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShaker : MonoBehaviour
{
    //Script was referenced from Sasquatch B Studios!
    public static CameraShaker instance;
    [SerializeField] private float globalShakeForce = 0.1f;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void CameraShake(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(globalShakeForce);
    }

}
