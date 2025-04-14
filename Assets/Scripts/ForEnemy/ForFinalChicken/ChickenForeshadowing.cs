using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenForeshadowing : MonoBehaviour
{
    public NewPlayerMovement pm;
    public PolygonCollider2D pc;
    public float waitingtime;
    public GameObject cShadow;
    public GameObject introObject;

    public AudioSource aus;
    public AudioClip woosh;

    public Vector2 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        aus = GetComponent<AudioSource>();
        pc = GetComponent<PolygonCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pc.enabled = false;
            StartCoroutine(ShadowIntro());
        }
    }

    private IEnumerator ShadowIntro()
    {
        if (pm != null)
        {
            pm.playerSpeed = 0f;
        }
        cShadow.SetActive(true);
        if(woosh != null)
        {
            aus.PlayOneShot(woosh);
        }
        yield return new WaitForSecondsRealtime(waitingtime);
        if (pm != null)
        {
            pm.playerSpeed = 1.7f;
        }
        pc.enabled = false;
        if(cShadow != null)
        {
            cShadow.SetActive(false);
            Destroy(cShadow);
        }
        introObject.SetActive(false);
        Destroy(gameObject);
        //music.SetActive(true);
    }
}
