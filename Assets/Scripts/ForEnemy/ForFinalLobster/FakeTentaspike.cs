using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTentaspike : MonoBehaviour
{
    public SpriteRenderer spikeRenderer;
    public float puddleDuration;
    public AudioSource aus;
    public AudioClip puddle;
    public AudioClip spike;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FakeTentacleLife());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator FakeTentacleLife()
    {
        Color color = spikeRenderer.color;
        color.a = 0f;
        spikeRenderer.color = color;

        float fadeInDuration = 0.1f;
        float elapsedIn = 0f;

        while (elapsedIn < fadeInDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedIn / fadeInDuration);
            spikeRenderer.color = new Color(color.r, color.g, color.b, alpha);
            elapsedIn += Time.deltaTime;
            yield return null;
        }
        spikeRenderer.color = new Color(color.r, color.g, color.b, 1f);

        if (aus != null)
        {
            aus.PlayOneShot(puddle);
        }
        yield return new WaitForSeconds(0.6f);
        if (aus != null)
        {
            aus.PlayOneShot(spike);
        }
    }
}
