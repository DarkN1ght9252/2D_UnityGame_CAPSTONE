using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New RangeWeapon", menuName = "Inventory/Item/Weapon/RangeWeapon")]
public class Item_Range_Weapon : Weapon
{
    public GameObject bulletPreFab;
    Weapon_type weapon = Weapon_type.Range;
}
