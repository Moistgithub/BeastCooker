using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterForeshadowing : MonoBehaviour
{
   // public GameObject introObject;
    public NewPlayerMovement pm;
    public PolygonCollider2D pc;
    public float waitingtime;
    public GameObject Spikes;
    public GameObject Barriers;
    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pc.enabled = false;
            StartCoroutine(SpikeIntro());
        }
    }
    private IEnumerator SpikeIntro()
    {
        if (pm != null)
        {
            pm.playerSpeed = 0f;
            pm.dodgeRollSpeed = 0f;
        }
        yield return new WaitForSecondsRealtime(0.5f);
        Barriers.SetActive(true);
        Spikes.SetActive(true);
        yield return new WaitForSecondsRealtime(waitingtime);
        if (pm != null)
        {
            pm.playerSpeed = 1.7f;
            pm.dodgeRollSpeed = 9f;
        }
        pc.enabled = false;
       // introObject.SetActive(false);

    }
}
