using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad;
    public Button button;
    // Start is called before the first frame update
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
    void OnButtonClick()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
