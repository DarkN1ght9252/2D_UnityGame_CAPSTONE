using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New MeleeWeapon", menuName = "Inventory/Item/Weapon/MeleeWeapon")]
public class Item_Melee_Weapon : Weapon
{
   public float Range = 1;
   Weapon_type weapon = Weapon_type.Melee;
   enum MeleeType{Default, Thrust, Swing};
   MeleeType meleetype = MeleeType.Default; 
}
