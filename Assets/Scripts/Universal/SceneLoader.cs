using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad;
    public Button button;
    public Animator UI;

    void Start()
    {
        if (button != null)
        {
            Debug.Log("it buttons");
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button reference is not assigned.");
        }
    }

    private void OnButtonClick()
    {
        StartCoroutine(SceneChange());
    }

    private IEnumerator SceneChange()
    {
        UI.SetBool("IsFadingIn", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneToLoad);
    }
}