using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class StateController : MonoBehaviour
{
    public EnemyStats enemystats;
    public State currentState;
    public State remainState;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public List<Transform> waypoints;
    [HideInInspector] public int nextWaypoint;
    [HideInInspector] public Transform target;
    [HideInInspector] public Vector3 lastknownTargetPos;
    [HideInInspector] public bool stateBoolVariable;
    [HideInInspector] public float stateTimeElapsed;

    private bool _isActive;
    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update() {
        if(!_isActive) return;
        currentState.UpdateState(this);
    }

    public void InitializeAI(bool activate, List<Transform> waypointList){
        waypoints = waypointList;
        nextWaypoint = 0;
        _isActive = activate;
        agent.enabled = _isActive;
        agent.updateRotation = false;
        Debug.Log("Agent Enabled");
    }
    public void TransitionToState(State nextState){
        if(nextState != remainState){
            currentState = nextState;
            OnExitState();
        }
    }
    public bool HasTimeElapsed(float duration){
        stateTimeElapsed += Time.deltaTime;
        if(stateTimeElapsed >= duration){
            stateTimeElapsed = 0;
            return true;
        }
        return false;
    }
    private void OnExitState(){
        stateBoolVariable = false;
        stateTimeElapsed = 0;
    }
    private void OnDrawGizmos() {
        if(currentState !=null){
            Gizmos.color = currentState.gizmocolor;
            Gizmos.DrawWireSphere(transform.position,1.5f);
        }
    }
}