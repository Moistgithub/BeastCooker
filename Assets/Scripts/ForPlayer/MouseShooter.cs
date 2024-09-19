using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseShooter : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletpos;
    public float timer;
    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("BreakableEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, enemy.transform.position);
        if(distance < 10)
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                timer = 0;
                MouseShoot();
            }
        }
    }

    void MouseShoot()
    {
        Instantiate(bullet, bulletpos.position, Quaternion.identity);
    }
}
