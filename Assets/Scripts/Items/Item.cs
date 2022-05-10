using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    #region Init variables
	public GameObject prefab;
    public bool isUnique;					//Bool for unique items
	public Sprite item_Sprite_worldSpace;	//Sprite for in worldspace Item
    public enum Item_type {Unset, Weapon, Equipment, Consumable};	//Item type 
    public Item_type item_type = Item_type.Unset;			//Itemtype
    public int item_amount = 0;				//Item amount for multiple to stack in inventory
    new public string name = "New Item";	// Name of the item
	public Sprite icon = null;				// Item icon
	public bool isDefaultItem = false;      // Is the item default wear?
	#endregion 

	// Called when the item is pressed in the inventory
	public virtual void Use ()
	{
		// Use the item
		// Something might happen
		Debug.Log("Using Item " + name);

		if(item_type == Item_type.Weapon){
			PlayerEquipment.instance.ChangeWeapon(this);
		}
	}
	//Called when removing from inventory
	public void RemoveFromInventory ()
	{
		Inventory.instance.Remove(this);
	}
}
