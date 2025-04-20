using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBoing : MonoBehaviour
{
    public Animator tentanimator;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Boing());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator Boing()
    {
        yield return new WaitForSeconds(1f);
        tentanimator.SetBool("Boing", false);
    }
}
