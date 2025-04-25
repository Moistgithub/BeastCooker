using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaffleCone : MonoBehaviour
{
    public float fadeDuration = 1.5f;
    public float lingerDuration = 1f;
    public float proximityThreshold = 0.2f;
    public GameObject shadowTarget;
    public GameObject killbox;

    private bool isFading = false;
    private float fadeTimer = 0f;
    private float lingerTimer = 0f;

    private SpriteRenderer waffleCone;
    public Rigidbody2D rb;
    public PolygonCollider2D pc;

    public float colliderDelay = 1.9f;
    private float colliderTimer = 0f;
    private bool colliderEnabled = false;
    public GameObject progenitor;
    public AudioSource aus;
    public AudioClip fall;
    public AudioClip boom;

    void Start()
    {
        waffleCone = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PolygonCollider2D>();

        if (waffleCone != null)
        {
            Color color = waffleCone.color;
            color.a = 1f;
            waffleCone.color = color;
        }

        if (pc != null)
            pc.enabled = false;

        lingerTimer = lingerDuration;
        colliderTimer = colliderDelay;
        if(aus!= null)
        {
            aus.PlayOneShot(fall);
        }
    }

    void Update()
    {
        if (!colliderEnabled)
        {
            colliderTimer -= Time.deltaTime;
            if (colliderTimer <= 0f)
            {
                if (aus != null)
                {
                    aus.PlayOneShot(boom);
                }
                pc.enabled = true;
                colliderEnabled = true;
            }
        }
        if (!isFading && shadowTarget != null)
        {
            float distance = Vector2.Distance(transform.position, shadowTarget.transform.position);

            if (distance <= proximityThreshold)
            {
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;

                lingerTimer -= Time.deltaTime;

                if (lingerTimer <= 0f)
                {
                    StartFading();
                }
            }
            else
            {
                rb.gravityScale = 1f;
                lingerTimer = lingerDuration;
            }
        }

        if (isFading && waffleCone != null)
        {
            fadeTimer -= Time.deltaTime;

            float alpha = Mathf.Clamp01(fadeTimer / fadeDuration);
            Color color = waffleCone.color;
            color.a = alpha;
            waffleCone.color = color;

            if (fadeTimer <= 0f)
            {
                killbox.SetActive(false);
                if (shadowTarget != null)
                {
                    Destroy(shadowTarget);
                }
                Destroy(progenitor);
            }
        }
    }

    void StartFading()
    {
        isFading = true;
        fadeTimer = fadeDuration;
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        pc.enabled = false;
    }
}