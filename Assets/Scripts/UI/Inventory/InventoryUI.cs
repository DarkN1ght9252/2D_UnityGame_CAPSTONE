using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class InventoryUI : MonoBehaviour
{
    
	public Transform itemsParent;	// The parent object of all the items
	public GameObject inventoryUI;	// The entire UI
	EventSystem eventSystem;

	Inventory inventory;	// Our current inventory

	InventorySlot[] slots;	// List of all the slots


	public Player_Actions playerControls;
	private InputAction inv;

	private void Awake() {
		playerControls = new Player_Actions();	
	}

	private void OnEnable() {
		playerControls.Enable();
		//Jump Input
		inv = playerControls.Player.Inventory;
		inv.Enable();
		inv.performed += Inv;
	}

	private void OnDisable() {
		playerControls.Disable();
		inv.Disable();
	}

	private void Inv(InputAction.CallbackContext context){
		Debug.Log("Player Inventory");
		InventoryToggle();		
	}

	public void InventoryToggle(){
		//Code turns on Inventory
		if(!PauseGame.GameIsPaused){

			inventoryUI.SetActive(!inventoryUI.activeSelf);
			
			if (Time.timeScale == 1.0f) {
				Time.timeScale = 0.0f;
				eventSystem.SetSelectedGameObject(itemsParent.transform.GetChild(0).gameObject); 
			}else{
				Time.timeScale = 1.0f;  
			}	
		}	
	}

	void Start () {
		eventSystem = EventSystem.current;
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;	// Subscribe to the onItemChanged callback

		// Populate our slots array
		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}

	// Update the inventory UI by:
	//		- Adding items
	//		- Clearing empty slots
	// This is called using a delegate on the Inventory.
	void UpdateUI ()
	{
		// Loop through all the slots
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.items.Count)	// If there is an item to add
			{
				slots[i].AddItem(inventory.items[i]);	// Add it
			} else
			{
				// Otherwise clear the slot
				slots[i].ClearSlot();
			}
		}
	}
}
