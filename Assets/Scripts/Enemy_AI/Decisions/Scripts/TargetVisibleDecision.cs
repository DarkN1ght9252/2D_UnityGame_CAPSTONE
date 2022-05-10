using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName="AI/Decisions/TargetNotVisible")]
public class TargetVisibleDecision : Decision
{
     public override bool Decide(StateController controller){
        return TargetNotVisible(controller);
    }

    private bool TargetNotVisible(StateController controller){
 
        controller.transform.Rotate(0,controller.enemystats.searchTurnSpeed * Time.deltaTime, 0);
        return controller.HasTimeElapsed(controller.enemystats.searchDuration);

    }
}