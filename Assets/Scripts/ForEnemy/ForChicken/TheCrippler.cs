using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCrippler : MonoBehaviour
{
    public EnemyAttackManager eam;
    public ChickenMovement cm;
    public float crippleDuration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Crippling()
    {
        Debug.Log("Crippling started!");
        cm.enabled = false;
        eam.enabled = false;
        yield return new WaitForSeconds(crippleDuration);
        cm.enabled = true;
        eam.enabled = true;
        Debug.Log("Crippling ended!");
    }
}
