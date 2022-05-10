using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float walkSpeed;
    public float runSpeed;
    public float attackRate;
    public float damage;
    public float searchDuration;
    public float searchTurnSpeed;
}
