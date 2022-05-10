using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="AI/Actions/Patrol")]
public class PatrolAction : Action
{
    public override void Act(StateController controller){
        Patrol(controller);
    }
    private void Patrol(StateController controller){
        controller.agent.destination = controller.waypoints[controller.nextWaypoint].position;
        controller.agent.Resume();
        if(controller.agent.remainingDistance <= controller.agent.stoppingDistance 
        && !controller.agent.pathPending){
            controller.nextWaypoint = (controller.nextWaypoint +1) % controller.waypoints.Count;
        }
    }
}
