using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class FinalCookingMinigame : MonoBehaviour
{
    public string nextScene;

    public float waitTime;
    public float minigameTime = 20f;
    public float currentTime;

    public GameObject oldWog;
    public GameObject wogPlayer;
    public GameObject ingredientSpawner;
    public GameObject fakerar;
    public GameObject deadrar;
    public GameObject fakeVegetables;

    public Animator whiteboxAnim;


    private bool isMinigameActive = false;

    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = minigameTime;
        StartCoroutine(StartMinigame());
    }

    // Update is called once per frame
    void Update()
    {
        if (isMinigameActive)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                StartCoroutine(Fade());
            }
        }
    }
    private IEnumerator StartMinigame()
    {
        waitTime = 2.5f;
        yield return new WaitForSeconds(waitTime);
        CamSwitch();
        StartCoroutine(Minigame());
    }
    private IEnumerator Minigame()
    {
        //minigameTime = Time.time;
        ingredientSpawner.SetActive(true);
        oldWog.SetActive(false);
        wogPlayer.SetActive(true);
        fakerar.SetActive(false);
        deadrar.SetActive(true);
        fakeVegetables.SetActive(true);
        Debug.Log("Minigame started!");
        isMinigameActive = true;

        yield return new WaitForSeconds(minigameTime);

        //Start a coroutine to stop the minigame after 10 seconds
        StartCoroutine(Fade());
    }
    public IEnumerator Fade()
    {
        whiteboxAnim.SetBool("IsFadingIn", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextScene);
    }
    private void CamSwitch()
    {
        CameraManager.SwitchCamera(cam2);

    }
}
