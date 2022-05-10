using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName="AI/State")]
public class State : ScriptableObject
{
    public Action[] actions;
    public Transition[] transitions;
    public Color gizmocolor;
    public void UpdateState(StateController controller){
        ExecuteActions(controller);
        CheckForTransitions(controller);
    }
    
    private void ExecuteActions(StateController controller){
        foreach(var action in actions){
            action.Act(controller);
        }
    }

    private void CheckForTransitions(StateController controller){
        foreach(var transition in transitions){
            bool decision = transition.decision.Decide(controller);
            if(decision){
                controller.TransitionToState(transition.trueState);
            }else{
                controller.TransitionToState(transition.falseState);
            }
        }
    }
}
