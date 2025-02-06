using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpecialCollider : MonoBehaviour
{
    public ParticleSystem specialEffect;
    public ParticleSystem specialEffect2;
    public ParticleSystem specialEffect3;
    public BossHealth bosshealth;
    public float specialDamage = 5f;
    public bool NextState = false;
    //public CinemachineVirtualCamera cam2;
    public CinemachineImpulseSource impulseSource;
    void Start()
    {
        bosshealth = GetComponent<BossHealth>();
        if (impulseSource == null)
        {
            impulseSource = GetComponent<CinemachineImpulseSource>();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered has the tag "SpecialProps"
        if (other.CompareTag("SpecialProps"))
        {
            /*if(cam2 != null)
            {
                CameraShaker.Instance.ShakeCamera(1f, 1f);
            }*/
            if (impulseSource != null)
            {
                CameraShaker.instance.CameraShake(impulseSource);
            }
            if (specialEffect != null && specialEffect2 != null && specialEffect3 != null)
            {
                specialEffect.Play();
                specialEffect2.Play();
                specialEffect3.Play();
            }
            // Destroy the object that triggered the collider
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Changer"))
        {
            Debug.Log("Next State activate");
            NextState = true;
            Destroy(other.gameObject);
            //bosshealth.TakeDamage(specialDamage);
        }
    }
}
