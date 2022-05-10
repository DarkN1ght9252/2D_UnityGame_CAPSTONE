using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public Vector3 playerSpawn_Scene1 = new Vector3(0,0,-1);
    [SerializeReference] Player player;
    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = playerSpawn_Scene1;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.Health <= 0){
            // player.transform.position = Player.;
            player.Health = 100;
        }

        if (player.transform.position.y == -60)
        {
            player.transform.position = playerSpawn_Scene1;
        }
    }
}
