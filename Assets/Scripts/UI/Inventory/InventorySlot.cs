using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
   	public Image icon;			// Reference to the Icon image
	public Text textfield; 

	Item item;  // Current item in the slot

	// Add item to the slot
	public void AddItem (Item newItem)
	{
		item = newItem;
		// textfield = this.gameObject.GetComponent<Text>();
		textfield.text = item.name +"\t("+ item.item_amount.ToString()+")";
		icon.sprite = item.icon;
		icon.enabled = true;
		textfield.enabled = true;
	}

	// Clear the slot
	public void ClearSlot ()
	{
		item = null;

		icon.sprite = null;
		icon.enabled = false;
	}

	// Called when the remove button is pressed
	public void OnRemoveButton ()
	{
		Inventory.instance.Remove(item);
	}

	// Called when the item is pressed
	public void UseItem ()
	{
		if (item != null)
		{
			item.Use();
		}
	}
}
