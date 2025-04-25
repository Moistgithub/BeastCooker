using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaffleCone : MonoBehaviour
{
    public float fadeDuration = 1.5f;
    private bool isFading = false;
    private float fadeTimer = 0f;
    private SpriteRenderer waffleCone;
    public Rigidbody2D rb;
    private GameObject shadowTarget;
    public PolygonCollider2D pc;

    void Start()
    {
        waffleCone = GetComponent<SpriteRenderer>();
        if (waffleCone != null)
        {
            rb = GetComponent<Rigidbody2D>();
            Color color = waffleCone.color;
            color.a = 1f;
            waffleCone.color = color;
            pc = rb.GetComponent<PolygonCollider2D>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shadow") && !isFading)
        {
            shadowTarget = other.gameObject;
            StartFading();
        }
    }

    void StartFading()
    {
        
        isFading = true;
        fadeTimer = fadeDuration;
    }

    void Update()
    {
        if (isFading && waffleCone != null)
        {
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
            fadeTimer -= Time.deltaTime;
            pc.enabled = false;
            float alpha = Mathf.Clamp01(fadeTimer / fadeDuration);

            Color color = waffleCone.color;
            color.a = alpha;
            waffleCone.color = color;

            if (fadeTimer <= 0f)
            {
                if (shadowTarget != null)
                {
                    Destroy(shadowTarget);
                }
                Destroy(gameObject);
            }
        }
    }
}