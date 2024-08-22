using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Vector3 offset;
    public float damping;
    public Transform objectOfInterest;

    //used for reference for smooth damp
    private Vector3 vel = Vector3.zero;


    private void FixedUpdate()
    {
        if(objectOfInterest == null)
        {
            return;
        }

        Vector3 ooiPosition = objectOfInterest.position + offset;

        ooiPosition.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, ooiPosition, ref  vel, damping);
    }

}
