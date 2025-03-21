using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenVisualHandler : MonoBehaviour
{
    [Header("Public Variables")]
    public GameObject chickenHealthySprite;
    public GameObject chickenHurtSprite;
    public GameObject chickenDyingSprite;

    [Header("References")]
    public ChickenStateManager csm;
    public NBossHealth bossHealth;
    // Start is called before the first frame update
    void Start()
    {
        csm = GetComponent<ChickenStateManager>();
        bossHealth = GetComponent<NBossHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (csm.currentStateName == "ChickenHealthyState")
        {
            chickenHurtSprite.SetActive(false);
            chickenDyingSprite.SetActive(false);
            Debug.Log("it works");
        }
        if (csm.currentStateName == "ChickenLightDamage")
        {
            chickenHealthySprite.SetActive(false);
            chickenHurtSprite.SetActive(true);
            Debug.Log("it hurts");
        }
        if (csm.currentStateName == "ChickenHeavyDamage")
        {
            chickenHurtSprite.SetActive(false);
            chickenDyingSprite.SetActive(true);
            Debug.Log("it burns");
        }

    }
}
