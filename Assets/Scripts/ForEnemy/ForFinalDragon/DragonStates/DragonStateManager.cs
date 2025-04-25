using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonStateManager : MonoBehaviour
{
    DragonBaseState currentState;
    public DragonCutsceneState cutsceneState = new DragonCutsceneState();
    public DragonHealthyState healthyState = new DragonHealthyState();
    public DragonDamagedAState damagedAState = new DragonDamagedAState();
    public DragonDamagedBState damagedBState = new DragonDamagedBState();
    public DragonDizzyState dizzyState = new DragonDizzyState();

    [SerializeField]
    public string currentStateName;
    // Start is called before the first frame update
    void Start()
    {
        currentState = cutsceneState;
        currentState.EnterState(this);
        currentStateName = currentState.GetType().Name;
        currentStateName = currentState.GetType().Name;
    }

    // Update is called once per frame 
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(DragonBaseState state)
    {
        currentState = state;
        state.EnterState(this);
        currentStateName = currentState.GetType().Name;
    }
}
