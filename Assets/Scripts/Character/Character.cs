using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IKillable, IDamageable<int>
{
    public int level;
    [SerializeField]int health;
    public int Health {set{health = value;} get{return health;}}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Damage(int damage){
        Health -= damage;
    }
    public void Kill(){
        Destroy(gameObject);
    }
}
