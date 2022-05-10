using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireNeedle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CanHitPlayer()){
            Debug.Log("Fire Needle");
            
        }
    }

     private bool CanHitPlayer(){
        RaycastHit2D fraycastHit = Physics2D.Raycast(this.transform.position,Vector2.left,1.0f);
        
        Color rayColor;

        if (fraycastHit.collider != null)
        {
            rayColor = Color.green;
            return true;
        }else {
            rayColor = Color.red;
        }
        Debug.DrawRay(this.transform.position, Vector2.left*(5.0f), rayColor, 5.0f);
        return false;
    }
    private void Fire(){

    }
}
