using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LobsterSaltCollider : MonoBehaviour
{
    public float fadeDuration = 0.5f;
    private bool isFading = false;
    private float fadeTimer = 0f;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public AudioSource aus;
   // public AudioSource backgroundMusic;
    public AudioClip boom;

    public CinemachineImpulseSource cis;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Salt") && !isFading)
        {
            HitStop.Instance.StopTime(0.05f);
            if(aus != null)
            {
                aus.PlayOneShot(boom);
            }
            if(cis != null)
            {
                CameraShaker.instance.CameraShake(cis);
            }
            isFading = true;
        }
    }

    void Update()
    {
        if (isFading && spriteRenderer != null)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(originalColor.a, 0f, fadeTimer / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            if (fadeTimer >= fadeDuration)
            {
                gameObject.SetActive(false);
            }
        }
    }

}
