using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBreakableAttack : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyattackPoint;
    private float attackTime = 0.3f;
    public float attackDamage;
    private float timer;
    private bool isAttacking = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isAttacking && collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth something = collision.gameObject.GetComponent<PlayerHealth>();
            if (something == null)
                return;
            something.TakeDamage(attackDamage);
            Debug.Log("player is hurt");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if(distance < 5)
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                timer = 0;
                Attack();
            }
        }
    }
    void Attack()
    {
        enemyattackPoint.SetActive(true);
        isAttacking = true;
        StartCoroutine(TimeHandler());
    }

    IEnumerator TimeHandler()
    {
        //Handles the countdown of 0.3 seconds for the attacks lifetime
        yield return new WaitForSeconds(attackTime);
        Dissapear();
        //Debug.Log("Time " + attackTime);
    }

    public void Dissapear()
    {
        //attack literally dies
        enemyattackPoint.SetActive(false);
        isAttacking = false;
        //Debug.Log("Im not running");
    }
}
