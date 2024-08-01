using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerMovement playermovement;
    public GameObject attackPoint;
    private float attackTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        playermovement = GetComponent<PlayerMovement>();
    }
    private void FixedUpdate()
    {
        attackPoint.transform.localPosition = playermovement.movementDir * 2;
    }
    public void Attack()
    {
        attackPoint.SetActive(true);
        Debug.Log("Im running");
    }

    IEnumerator TimeHandler()
    {
        Attack();
        yield return new WaitForSeconds(attackTime);
        Dissapear();
        Debug.Log("Time " + attackTime);
    }   
    public void Dissapear()
    {
        attackPoint.SetActive(false);
        Debug.Log("Im not running");
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("z"))
        {
            StartCoroutine(TimeHandler());
        }
    }
}
