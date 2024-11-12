using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    public static HitStop Instance;
    private void Awake()
    {
        //makes sure theres only one instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    //time stop
    public void StopTime(float duration)
    {
        StartCoroutine(StopTimeCoroutine(duration));
    }

    private IEnumerator StopTimeCoroutine(float duration)
    {
        //stop the time by setting timescale to 0
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        //resume time
        Time.timeScale = 1f;
    }
}
