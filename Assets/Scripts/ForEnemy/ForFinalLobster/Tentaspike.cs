using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentaspike : MonoBehaviour
{
    public SpriteRenderer spikeRenderer;
    public GameObject hurtbox;
    public float puddleDuration;
    public float activeDuration;
    public float fadeDuration;
    public AudioSource aus;
    public AudioClip puddle;
    public AudioClip spike;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TentacleLife());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TentacleLife()
    {
        Color color = spikeRenderer.color;
        color.a = 0f;
        spikeRenderer.color = color;

        float fadeInDuration = 0.2f;
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
        yield return new WaitForSeconds(0.55f);
        if (aus != null)
        {
            aus.PlayOneShot(spike);
        }
        hurtbox.SetActive(true);

        float timer = 0f;
        while (timer < activeDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(fadeDuration);
        hurtbox.SetActive(false);
        float elapsed = 0f;
        Color originalColor = spikeRenderer.color;

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            spikeRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
