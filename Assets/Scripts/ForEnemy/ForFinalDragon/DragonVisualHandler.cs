using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonVisualHandler : MonoBehaviour
{
    public Animator currentAnimator;
    public DragonStateManager dsm;
    // Start is called before the first frame update
    void Start()
    {
        currentAnimator = GetComponentInChildren<Animator>();
        dsm = GetComponent<DragonStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dsm.currentStateName == "DragonHealthyState")
        {
            //currentAnimator.SetBool("idle", false);
            Debug.Log("Healthy Dragon");
        }
        if (dsm.currentStateName == "LobsterDamagedAState")
        {


            Debug.Log("Lobster Ouch A");
        }
        if (dsm.currentStateName == "LobsterDamagedB")
        {

            Debug.Log("Lobster Oucvh");
        }
        if (dsm.currentStateName == "LobsterDizzyState")
        {
        }
    }
}
