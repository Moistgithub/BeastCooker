using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public float scrollSpeed = 30f;
    public RectTransform transform;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
    }
}
