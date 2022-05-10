using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Item/Consumable")]
public class Consumable : Item
{
   public void Awake() {
       item_type = Item_type.Consumable;
   }
}
