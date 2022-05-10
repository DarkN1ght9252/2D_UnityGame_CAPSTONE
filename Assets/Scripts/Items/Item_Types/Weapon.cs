using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Item/Weapon")]
public class Weapon : Item
{
    public float damage = 0;
    public float weaponCoolDown = 1;
    public float[] crouchedXYZ = new float[3];
    public float[] nonCrouchedXZ = new float[3];
    public enum Weapon_type {Unset, Melee, Range};	//Item type 
    public Weapon_type weapon_type = Weapon_type.Unset;			//Itemtype
}