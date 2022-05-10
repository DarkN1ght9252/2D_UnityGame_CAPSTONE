using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName="AI/Decisions/Look")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller){
        return Look(controller);
    }

    private bool Look(StateController controller){
        bool isTrue = false;
        FieldOfView fov = controller.GetComponent<FieldOfView>();
        
        if(fov != null && fov.CanSeePlayer){
            controller.target = fov.playerRef.transform;
            isTrue = true;
        }
        return isTrue;
    }
}
