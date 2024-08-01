using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerMovement playermovement;
    public GameObject attackPoint;
    private float attackTime = 0.3f;
    private Vector3 lastAttackPosition;

    // Start is called before the first frame update
    void Start()
    {
        playermovement = GetComponent<PlayerMovement>();
    }
    public void Attack()
    {
        //creates scenario where the player isnt moving but turns the attack direction to the last used
        if (playermovement.movementDir.x != 0 || playermovement.movementDir.y != 0)
        {
            lastAttackPosition = playermovement.movementDir;
        }
        //uses reference from player movement to change location of attack point

        attackPoint.transform.localPosition = lastAttackPosition; //playermovement.movementDir;

        attackPoint.SetActive(true);
        StartCoroutine(TimeHandler());
        Debug.Log("Im running");
    }

    IEnumerator TimeHandler()
    {
        //Handles the countdown of 0.3 seconds for the attacks lifetime
        yield return new WaitForSeconds(attackTime);
        Dissapear();
        Debug.Log("Time " + attackTime);
    }   
    public void Dissapear()
    {
        //attack literally dies
        attackPoint.SetActive(false);
        Debug.Log("Im not running");
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }
}
