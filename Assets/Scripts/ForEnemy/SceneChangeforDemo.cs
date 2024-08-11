using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeforDemo : MonoBehaviour
{
    public string endingScene;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object we collided with has the tag "stove"
        if (collision.gameObject.CompareTag("Stove"))
        {
            // Load the specified scene
            SceneManager.LoadScene(endingScene);
        }
    }
}
