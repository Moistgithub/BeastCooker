using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousehider : MonoBehaviour
{
    public PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth != null && playerHealth.currentHealth >= 0f)
        {
            Cursor.visible = true;
        }
        if (playerHealth == null)
            return;
    }
}
