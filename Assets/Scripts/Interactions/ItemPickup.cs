using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IPickupable
{
   public Item item;

	public bool hasBeenGrabbed = false;
   public void PickupItem(){

		Debug.Log("Picking up " + item.name);
		if(item == null){
			Debug.Log("Item is NULL");
		}
		Inventory.instance.Add((Item)item);	// Add to inventory
	}
	private void OnTriggerStay2D(Collider2D other) {
		if(!hasBeenGrabbed && other.gameObject.layer == 3){
			hasBeenGrabbed = true;
			PickupItem();
			gameObject.SetActive(false);
			kill();	// Destroy item from scene
		}
	}
	void kill(){
		Destroy(this.gameObject);
	}
}