using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    #region Init variables
    private string npc_name;
    enum NPC_type {Unset, Hostile, nonHostile};
    private NPC_type NPC_Type;

    private GameObject NPC_prefab;
    #endregion   
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
