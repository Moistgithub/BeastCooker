using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotSpawner : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletpos;
    public float timer;
    private GameObject player;
    public float timervalue;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }
        float distance = Vector2.Distance(transform.position, player.transform.position);
        timer += Time.deltaTime;
        if (timer > timervalue)
        {
            timer = 0;
            EnemyShoot();
        }
    }
    void EnemyShoot()
    {
        Instantiate(bullet, bulletpos.position, Quaternion.identity);
    }
}
