using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonLaser : MonoBehaviour
{
    [Header("References")]
    public GameObject scaryPrefab;
    public AudioClip beepSound;
    public AudioClip lockOn;
    public float targetingDuration = 3f;
    public float delayBetweenAttacks = 2f;

    private GameObject player;
    private LineRenderer lineRenderer;
    private AudioSource audioSource;

    /*private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null) return;

        lineRenderer = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();

        if (lineRenderer == null || audioSource == null) return;

        StartCoroutine(TargetAndSpawnLoop());
    }*/
    private void OnEnable()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (player != null && lineRenderer != null && audioSource != null)
        {
            StartCoroutine(TargetAndSpawnLoop());
        }
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        lineRenderer.enabled = false;
    }
    private IEnumerator TargetAndSpawnLoop()
    {
        while (true)
        {
            Vector3 spawnPoint = Vector3.zero;

            // Beep once
            if (audioSource && beepSound)
                audioSource.PlayOneShot(beepSound);

            yield return new WaitForSeconds(1f);

            // Lock on sound
            if (audioSource && lockOn)
                audioSource.PlayOneShot(lockOn);

            float elapsed = 0f;

            while (elapsed < targetingDuration)
            {
                if (player != null)
                {
                    Vector3 playerPos = player.transform.position;
                    spawnPoint = playerPos;

                    lineRenderer.enabled = true;
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, playerPos);
                }

                elapsed += Time.deltaTime;
                yield return null;
            }

            lineRenderer.enabled = false;

            if (scaryPrefab != null && player != null)
            {
                Instantiate(scaryPrefab, transform.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(delayBetweenAttacks);
        }
    }
}
