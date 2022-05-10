using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_Blocks : MonoBehaviour, IDamageable<int>, IKillable, ICollider
{
    public int Health{set;get;} = 4;

    public void OnCollisionEnter2D(Collision2D other){
        Debug.Log("Wall collision");
        
        if(other.gameObject.GetComponent<Bullet>() is Bullet){
            Damage(1);
            Debug.Log("wall was hit");
        }
        if(Health == 0){
           Kill();
        }
    }

    public void Damage(int damage){
        Health-=damage;
    }
    public void Kill(){
        Destroy(gameObject);
    }
}
