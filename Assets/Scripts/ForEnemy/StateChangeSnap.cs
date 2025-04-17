using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChangeSnap : MonoBehaviour
{
    public Transform player;

    public AudioSource audioSource;
    public AudioClip snapSound;
    public AudioClip killSound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StateSoundTransitioner()
    {
        if(snapSound != null)
        {
            audioSource.PlayOneShot(snapSound);
            HitStop.Instance.StopTime(2f);
        }
    }
    public void KillTransitioner()
    {
        if (killSound != null)
        {
            audioSource.PlayOneShot(killSound);
            //HitStop.Instance.StopTime(2f);
        }
    }
}
