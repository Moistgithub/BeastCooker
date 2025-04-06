using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoxEnemy : MonoBehaviour
{
    public float damage;
    public float hsDur;
    public NBossHealth bossHealth;
    // Start is called before the first frame update
    void Start()
    {
        bossHealth = FindObjectOfType<NBossHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Boss"))
        {
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
                HitStop.Instance.StopTime(hsDur);
                Debug.Log("damaging");
            }
        }
    }
}
