using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WogFinalChanger : MonoBehaviour
{
    public GameObject SceneChanger;
    public Canvas targetCanvas;
    // Start is called before the first frame update
    void Start()
    {
        // Find the image object with the name "final fade" in the target canvas
        Image finalFadeImage = FindImageInCanvas("FinalFade");

        if (finalFadeImage != null)
        {
            // If the object is active, set the image to active
            if (gameObject.activeInHierarchy)
            {
                finalFadeImage.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogWarning("Image with the name 'final fade' not found in the canvas.");
        }
    }

    // Function to find an image in the canvas by name
    private Image FindImageInCanvas(string imageName)
    {
        SceneChanger.SetActive(true);
        // Look through all the UI elements in the canvas and find the one with the matching name
        foreach (Transform child in targetCanvas.transform)
        {
            if (child.gameObject.name == imageName)
            {
                // Return the Image component of the found object
                return child.GetComponent<Image>();
            }
        }
        return null; // Return null if the image is not found
    }
}
