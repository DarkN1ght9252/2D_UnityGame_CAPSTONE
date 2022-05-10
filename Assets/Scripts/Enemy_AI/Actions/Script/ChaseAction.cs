using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName="AI/Actions/Chase")]
public class ChaseAction : Action
{
    public override void Act(StateController controller)
    {
        Chase(controller);
    }
    private void Chase(StateController controller){
        FieldOfView fov = controller.GetComponent<FieldOfView>();
        if(fov == null) return;
        if(fov.CanSeePlayer){
            controller.agent.destination = controller.target.position;
            controller.lastknownTargetPos = controller.target.position;
            controller.agent.Resume();
        }else{
            controller.agent.destination = controller.lastknownTargetPos;
            controller.agent.Resume();
        }
    }
}
