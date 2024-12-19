using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrippler : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public EnemyHealth enemyHealth;
    public PlayerAttack pa;
    public float crippleDuration;
    public bool activeCripple = false;
    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth == null)
            return;

        if (enemyHealth.currentHealth == 25 && !activeCripple)
        {
            StartCoroutine(PlayerCrippling());
        }
    }
    public IEnumerator PlayerCrippling()
    {
        activeCripple = true;
        playerMovement.isInvincible = true;

        pa.enabled = false;
        yield return new WaitForSeconds(crippleDuration);
        pa.enabled = true;

        playerMovement.isInvincible = false;
        activeCripple = false;
    }
}
