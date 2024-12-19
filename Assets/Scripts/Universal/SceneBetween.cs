using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBetween : MonoBehaviour
{
    public string nextScene;
    public Animator blackBoxAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Fade());
        }
    }
    public IEnumerator Fade()
    {
        blackBoxAnim.SetBool("IsFadingIn", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextScene);
    }
}
