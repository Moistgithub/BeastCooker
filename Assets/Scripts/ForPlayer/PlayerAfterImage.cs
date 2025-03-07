using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    [SerializeField]
    public float ActiveTime;
    private float timeActivated;
    private float alpha;
    [SerializeField]
    private float alphaSet;
    private float alphaMultiplier = 0.05f;

    [SerializeField]
    private Color spriteColor = Color.white;

    private Transform player;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer playerSprite;

    private Color color;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSprite = player.GetComponentInChildren<SpriteRenderer>();

        alpha = alphaSet;
        spriteRenderer.sprite = playerSprite.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        timeActivated = Time.time;
    }

    private void FixedUpdate()
    {
        alpha *= alphaMultiplier;
        color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
        spriteRenderer.color = color;

        if (Time.time > +(timeActivated + ActiveTime))
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);

        }
    }
}
