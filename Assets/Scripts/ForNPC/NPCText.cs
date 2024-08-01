using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NPCText : MonoBehaviour
{
    public GameObject talkableBubble;
    private bool inReach;

    // Start is called before the first frame update
    void Start()
    {
        talkableBubble.SetActive(false);
        inReach = false;
        //Gamelogic = GameObject.FindWithTag("GameLogic");
    }

    //what allows us to reach
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPCReacher")
        {
            inReach = true;
            talkableBubble.SetActive(true);

            Debug.Log("colliding");
        }
    }
    //what stops us from reaching
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NPCReacher")
        {
            inReach = false;
            talkableBubble.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (inReach && Input.GetButtonDown("e"))
        {
            talkableBubble.SetActive(false);
            //inReach = false;
        }
    }

}
