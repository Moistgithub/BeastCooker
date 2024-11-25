using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WogLerp : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    public float lerpSpeed;
    private float lerpTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //lerp time based on speed
        lerpTime += Time.deltaTime * lerpSpeed;
        //lerps object to position
        transform.position = Vector3.Lerp(startPos, endPos, lerpTime);
    }
    public void StartLerping(Vector3 newStartPos, Vector3 newEndPos)
    {
        startPos = newStartPos;
        endPos = newEndPos;
        lerpTime = 0.0f;
    }
}
