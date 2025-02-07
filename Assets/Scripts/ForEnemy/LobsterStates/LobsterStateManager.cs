using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterStateManager : MonoBehaviour
{
    LobsterBaseState currentState;
    public LobsterHealthyState healthyState = new LobsterHealthyState();
    public LobsterDamagedAState damagedAState = new LobsterDamagedAState();
    public LobsterDizzyState dizzyState = new LobsterDizzyState();
    public LobsterDizzierState dizzierState = new LobsterDizzierState();
    public LobsterDead deadState = new LobsterDead();

    [SerializeField]
    public string currentStateName;

    // Start is called before the first frame update
    void Start()
    {   
        currentState = healthyState;
        currentState.EnterState(this);
        currentStateName = currentState.GetType().Name;
        currentStateName = currentState.GetType().Name;
    }

    // Update is called once per frame 
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(LobsterBaseState state)
    {
        currentState = state;
        state.EnterState(this);
        currentStateName = currentState.GetType().Name;
    }
}
