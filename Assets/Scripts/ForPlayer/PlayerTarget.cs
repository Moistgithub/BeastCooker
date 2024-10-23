using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour
{
    public float lifespan;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifespan);
    }

}
