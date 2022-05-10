using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyThorn : MonoBehaviour
{
   public float speed = 15f;
    private Rigidbody2D bullet;
    private BoxCollider2D _box;
    private NavMeshAgent agent;
    [SerializeField] private LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        bullet = GetComponent<Rigidbody2D>();
        _box = GetComponent<BoxCollider2D>();


        Debug.Log("PLayer Local scale " + agent.desiredVelocity.x);
        if(agent.desiredVelocity.x < 0){
            bullet.velocity = transform.right * -speed;
            bullet.velocity = transform.up  * -speed;
        }
        else{
            bullet.transform.localScale = new Vector3(-bullet.transform.localScale.x,bullet.transform.localScale.y,bullet.transform.localScale.z);
            bullet.velocity = transform.right * speed;
            bullet.velocity = transform.up  * -speed;
        }
        
        _box = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Bullet Collision");
        if(other.gameObject.tag == "Flying"){
            Debug.Log("Flying Tag Detected");
            if (other.gameObject.GetComponentInParent<Hostile>() is Hostile)
            {
                Debug.Log("(Ranged) Hostile is Hit");
                other.gameObject.GetComponentInParent<Hostile>().Damage((int)Random.Range(3,10f));
            }else if( other.gameObject.GetComponent<Object>() is Breakable_Blocks){
                Debug.Log("Breakable wall Hit");
            }
        }else{
            if (other.gameObject.GetComponent<Hostile>() is Hostile)
            {
                Debug.Log("(Ranged) Hostile is Hit");
                other.gameObject.GetComponent<Hostile>().Damage((int)Random.Range(3,10f));
            }else if( other.gameObject.GetComponent<Object>() is Breakable_Blocks){
                Debug.Log("Breakable wall Hit");
            }
        }
        
        
        Debug.Log("Bullet Destroyed");
        Destroy(gameObject);
    }
    public void SetAgent(NavMeshAgent current){
        agent = current;
    }
}
