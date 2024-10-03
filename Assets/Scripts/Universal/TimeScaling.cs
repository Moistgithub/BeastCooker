using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaling : MonoBehaviour
{
    public GameObject ingredient;
    public GameObject ingredientb;
    public float tsDuration = 3f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ingredient = GameObject.FindGameObjectWithTag("Ingredient");
        if (ingredient == null)
        {
            StartCoroutine(StopTime());
        }
        ingredientb = GameObject.FindGameObjectWithTag("Ingredient");
        if (ingredient == null)
        {
            StartCoroutine(StopTime());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StopTime());
        }
    }
    private IEnumerator StopTime()
    {
        //Stop time
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(tsDuration);

        //Resume time
        Time.timeScale = 1f;
    }
}
