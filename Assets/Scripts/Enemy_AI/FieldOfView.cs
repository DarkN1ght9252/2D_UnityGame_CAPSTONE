using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius = 5f;
    [Range (1,360)] public float angle = 45f;
    public LayerMask targetLayer;
    public LayerMask ObstructionLayer;
    public GameObject playerRef;
    public bool CanSeePlayer;

    private void Start() {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVCheck());
    }
    private IEnumerator FOVCheck(){
        WaitForSeconds wait = new WaitForSeconds(.2f);
        while(true){
            yield return wait;
            FOV();
        }
    }
    //Creates FOV for enemy AI use
    private void FOV(){
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position,radius,targetLayer);
        if(rangeCheck.Length > 0){
            Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            if(Vector2.Angle(transform.up, directionToTarget) < angle /2){
                float distanceToTarget = Vector2.Distance(transform.position,target.position);
                if(Physics2D.Raycast(transform.position,directionToTarget,distanceToTarget,ObstructionLayer)){
                    CanSeePlayer = true;
                }else{
                    CanSeePlayer = false;
                }
            }else{
                CanSeePlayer = false;
            }
        }else if(CanSeePlayer){
            CanSeePlayer = false;
        }
    }


    //Draws the FOV of Enemy
    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position,Vector3.forward,radius);
        Vector3 angle1 = DirectionFromAngle(-transform.eulerAngles.z, -angle/2);
        Vector3 angle2 = DirectionFromAngle(-transform.eulerAngles.z, angle/2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position,transform.position + angle1 * radius);
        Gizmos.DrawLine(transform.position,transform.position + angle2 * radius);

        if(CanSeePlayer){
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, playerRef.transform.position);
        }
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees){
        angleInDegrees += eulerY;
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad)
        , Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
