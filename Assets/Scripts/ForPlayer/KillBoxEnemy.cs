using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class KillBoxEnemy : MonoBehaviour
{
    public float damage;
    public float hsDur;
    public NBossHealth bossHealth;
    public CinemachineImpulseSource cis;
    public CinemachineImpulseSource cis2;
    public PlayerAttack pa;

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
                if(pa != null)
                {
                    if(pa.shakeValue == 0)
                    {
                        if (cis != null)
                        {
                            CameraShaker.instance.CameraShake(cis);
                        }
                    }
                    else if (pa.shakeValue == 1)
                    {
                        if (cis != null)
                        {
                            CameraShaker.instance.CameraShake(cis2);
                        }
                    }
                }
                Debug.Log("damaging");
            }
        }
    }
}
