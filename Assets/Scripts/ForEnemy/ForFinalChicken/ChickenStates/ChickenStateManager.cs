using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenStateManager : MonoBehaviour
{
    ChickenBaseState currentState;
    public ChickenIdleState idleState = new ChickenIdleState();
    public ChickenHealthyState healthyState = new ChickenHealthyState();
    public ChickenLightDamage hurtState = new ChickenLightDamage();
    public ChickenHeavyDamage dyingState = new ChickenHeavyDamage();
    public ChickenDizzyState dizzyState = new ChickenDizzyState();
    public ChickenCutsceneIdleState cutsceneState = new ChickenCutsceneIdleState();

    [SerializeField]
    public string currentStateName;
    // Start is called before the first frame update
    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
        currentStateName = currentState.GetType().Name;
        currentStateName = currentState.GetType().Name;
    }

    // Update is called once per frame 
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(ChickenBaseState state)
    {
        currentState = state;
        state.EnterState(this);
        currentStateName = currentState.GetType().Name;
    }
}
