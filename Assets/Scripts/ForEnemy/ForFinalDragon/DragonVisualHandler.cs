using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonVisualHandler : MonoBehaviour
{
    public Animator currentAnimator;
    // Start is called before the first frame update
    void Start()
    {
        currentAnimator = GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
