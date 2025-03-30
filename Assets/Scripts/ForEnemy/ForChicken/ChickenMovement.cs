using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMovement : MonoBehaviour
{
    [Header("Public Variables")]
    public float speed = 1f;
    public float detectionRange;
    public GameObject Player;

    [Header("Private Variables")]
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.FindGameObjectWithTag("PlayerTarget");
        if (Player == null)
        {
            return;
        }

        //calculate distance between enemy and player
        distance = Vector2.Distance(transform.position, Player.transform.position);

        //enemy move to player droppings if in range
        if (distance < detectionRange && speed > 0)
        {
            Vector2 direction = Player.transform.position - transform.position;
            direction.Normalize();

            //moves to player droppings
            transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, speed * Time.deltaTime);

        }
    }
}

