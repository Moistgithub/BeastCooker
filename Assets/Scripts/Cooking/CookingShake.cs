using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CookingShake : MonoBehaviour
{
    public WogInventory wi;
    public CinemachineImpulseSource cis;

    // Start is called before the first frame update
    void Start()
    {
        cis = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(wi != null && wi.addIngredients)
        {
            CameraShaker.instance.CameraShake(cis);
        }
    }
}
