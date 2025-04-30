using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickSceneLoader : MonoBehaviour
{
    public string sceneToLoad;  // The name of the scene to load, which you can change in the Inspector.
    public AudioSource aus;
    public AudioSource music;
    public AudioClip ding;
    public Animator black;
    public bool hasClicked = false;

    // Update is called once per frame
    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0) && !hasClicked) // 0 corresponds to the left mouse button
        {
            StartCoroutine(Thingy());
        }
    }

    private IEnumerator Thingy()
    {
        hasClicked = true;
        if(aus != null)
        {
            music.Stop();
            aus.PlayOneShot(ding);
        }
        if (black != null)
        {
            black.SetBool("IsFadingIn", true);
        }
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(sceneToLoad);
    }
}