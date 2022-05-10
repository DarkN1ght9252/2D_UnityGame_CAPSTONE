using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnTime = 3f;
    [SerializeReference] GameObject enemy1;
    private GameObject enemy = null;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy());
    }
    IEnumerator spawnEnemy(){
        while(true){
            if(!enemy){
                enemy = Instantiate<GameObject>(enemy1);
                enemy1.transform.position = new Vector3(this.transform.position.x,this.transform.position.y+1,this.transform.position.z);
            }
            yield return 0;
        }
    }
}
