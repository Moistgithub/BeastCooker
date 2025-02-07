using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimedSceneChange : MonoBehaviour
{
    public float WaitingTime = 5f;
    public float fadeDuration = 5f;
    public string SceneName;
    // Reference to the Image used for the fade effect
    public Image fadeImage;
    void Start()
    {
        fadeImage.color = new Color(1f, 1f, 1f, 0f);

        // Start the scene change after a random time
        StartCoroutine(ChangeSceneAfterDelay());
        StartCoroutine(ChangeSceneAfterDelay());
    }

    IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(WaitingTime);
        yield return StartCoroutine(FadeToWhite());

        SceneManager.LoadScene(SceneName);
    }
    IEnumerator FadeToWhite()
    {
        float elapsedTime = 0f;

        // Fade the image from transparent to fully white
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        // Ensure it's fully white at the end
        fadeImage.color = new Color(1f, 1f, 1f, 1f);
    }
}