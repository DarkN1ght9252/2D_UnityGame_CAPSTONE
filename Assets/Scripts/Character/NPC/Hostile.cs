using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostile : NPC
{
    private CircleCollider2D meleeCollider;
    
    void Start() {
        //Ignore Enemy/Player collision
        Physics2D.IgnoreLayerCollision(7, 3);
        //Ignore Enemy/Enemy collision
        Physics2D.IgnoreLayerCollision(7,7);
        //Grab meleeCollider
        meleeCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0){
            Kill();
        }
    }

    public void  TakeDamage(float damage){
        Debug.Log("Damage Taken = " + damage);
        if (Health <= 0){
            Health-= (int)damage;
        }
    }

    #region Detections
     public void OnCollisionEnter2D(Collision2D other) {
         //If is Player , player take damage
        if(other.gameObject.GetComponent<Player>() is Player){
            Debug.Log("player takes damage");
            other.gameObject.GetComponent<Player>().Damage(5);
        } 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Contact with Object Trigger");
        if(other.gameObject.layer == 3){
            other.gameObject.GetComponent<Player>().Damage(10);
        }
    }
    #endregion
   
}
