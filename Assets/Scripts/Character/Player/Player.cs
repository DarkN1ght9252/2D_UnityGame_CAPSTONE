using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, ICollider
{   
    public float[] lastSavePosition;

    // Start is called before the first frame update
    void Start() {
        Physics2D.IgnoreLayerCollision(3,7);
        Debug.Log("Player Loaded Game");
        lastSavePosition = new float[3];
        LoadPlayer();
    }
    void Update() {
        if(Input.GetKeyDown(KeyCode.R)){
            Debug.Log("Saved Game");
            SavePlayer();
        }
    }
    public void SavePlayer(){
        SaveSystem.SavePlayerData(this);
    }
    public void LoadPlayer(){
        PlayerData data = SaveSystem.loadPlayer();
        if(data != null){
            level = data.level;
            Health = data.health;
            if(data.position != null){
                lastSavePosition[0] = data.position[0];
                lastSavePosition[1] = data.position[1];
                lastSavePosition[2] = data.position[2];
            }
            transform.position = new Vector3(data.position[0],data.position[1],data.position[2]);
        }
        
    }

    public virtual void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == 7)
        {
			
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 7){
            Damage(10);
        }
    }
}
