using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.UI;

public class DragonIntro : MonoBehaviour
{
    [Header("Public Variables")]

    public CinemachineVirtualCamera playerCam;
    public CinemachineVirtualCamera dragonCam;
    public AudioSource aus;
    public AudioClip roar;
    public AudioClip roar2;
    public AudioClip roar3;
    public DragonVisualHandler dvh;
    public CinemachineImpulseSource cis;
    public DragonStateManager dsm;

    public AudioSource ambiencemusic;
    public AudioSource music;

    public NewPlayerMovement pm;
    public PolygonCollider2D pc;
    public GameObject CONE;
    public GameObject Hydra;
    public Vector3 Realflylocation;
    public Vector3 Realstartpos;

    public TextMeshProUGUI introText;
    public Image whiteFlashImage;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pc.enabled = false;
            StartCoroutine(BossIntro());
        }
    }
    private IEnumerator BossIntro()
    {
        if (ambiencemusic != null)
        {
            ambiencemusic.Stop();
        }

        CONE.SetActive(true);

        if (pm != null)
        {
            pm.playerSpeed = 0f;
            pm.dodgeRollSpeed = 0f;
        }

        CameraManager.SwitchCamera(dragonCam);

        float duration = 1.5f;
        float elapsedTime = 0f;


        yield return new WaitForSeconds(1f);

        // Lerp HydraFalse
        elapsedTime = 0f;

        yield return new WaitForSeconds(2f);

        // Lerp Hydra (real one)
        elapsedTime = 0f;
        Vector3 realdragonStart = Hydra.transform.position;

        while (elapsedTime < duration)
        {
            Hydra.transform.position = Vector3.Lerp(realdragonStart, Realflylocation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Hydra.transform.position = Realflylocation;

        yield return new WaitForSeconds(1f);

        if (aus != null)
        {
            aus.PlayOneShot(roar);
            aus.PlayOneShot(roar2);
            aus.PlayOneShot(roar3);
        }
        if (cis != null)
        {
            CameraShaker.instance.CameraShake(cis);
        }
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(TitleCard());

        yield return new WaitForSecondsRealtime(0.7f);
        CameraManager.SwitchCamera(playerCam);

        if (music != null)
        {
            music.Play();
        }

        if (pm != null)
        {
            pm.playerSpeed = 1.7f;
            pm.dodgeRollSpeed = 9f;
            yield return new WaitForSecondsRealtime(0.5f);
        }

        yield return new WaitForSecondsRealtime(3f);

        if (dsm != null)
        {
            dsm.SwitchState(dsm.healthyState);
        }

        pc.enabled = false;
    }

    private IEnumerator TitleCard()
    {
        float fadeDuration = 0.5f;
        float displayTime = 2f;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;

            SetAlpha(introText, normalizedTime);
            SetAlpha(whiteFlashImage, normalizedTime);

            yield return null;
        }

        SetAlpha(introText, 1f);
        SetAlpha(whiteFlashImage, 1f);

        yield return new WaitForSeconds(displayTime);

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = 1f - (t / fadeDuration);

            SetAlpha(introText, normalizedTime);
            SetAlpha(whiteFlashImage, normalizedTime);

            yield return null;
        }

        SetAlpha(introText, 0f);
        SetAlpha(whiteFlashImage, 0f);
    }

    private void SetAlpha(Graphic graphic, float alpha)
    {
        if (graphic == null) return;

        Color color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }
}
