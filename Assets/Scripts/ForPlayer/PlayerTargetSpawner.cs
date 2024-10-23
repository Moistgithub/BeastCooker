using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetSpawner : MonoBehaviour
{
    public GameObject playerTarget;
    public Transform ptPos;
    public float timer;
    public float timervalue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timervalue)
        {
            timer = 0;
            Spawn();
        }
    }
    void Spawn()
    {
        Instantiate(playerTarget, ptPos.position, Quaternion.identity);
    }
}
