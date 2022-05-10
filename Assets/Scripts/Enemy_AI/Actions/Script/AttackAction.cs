using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName="AI/Actions/Attack")]
public class AttackAction : Action
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }
    private void Attack(StateController controller){
        FieldOfView fov = controller.GetComponent<FieldOfView>();
        if(fov == null) return;
        if(!controller.stateBoolVariable){
            controller.stateTimeElapsed = controller.enemystats.attackRate;
            controller.stateBoolVariable = true;
        }
        if(fov.CanSeePlayer){
            if(controller.HasTimeElapsed(controller.enemystats.attackRate)){
                //Attack Player
                Debug.Log("Attack PLayer");
            }
        }
    }
}
