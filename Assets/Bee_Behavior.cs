using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bee_Behavior : MonoBehaviour
{
    
    SpriteRenderer sprite;
    [HideInInspector] public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        //Fix Rotation Error
        this.transform.eulerAngles = new Vector3(0,0,0);
        agent = GetComponent<NavMeshAgent>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update() {
        //Flips Sprite Based on AI
        flipSprite();
    }
   
    private void flipSprite(){
       if(agent.desiredVelocity.x > 0){
           sprite.flipX = true;
       }else{
           sprite.flipX = false;
       }
    }
}
