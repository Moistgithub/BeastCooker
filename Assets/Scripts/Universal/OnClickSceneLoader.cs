using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickSceneLoader : MonoBehaviour
{
    public string sceneToLoad;  // The name of the scene to load, which you can change in the Inspector.

    // Update is called once per frame
    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0)) // 0 corresponds to the left mouse button
        {
            // Load the scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}