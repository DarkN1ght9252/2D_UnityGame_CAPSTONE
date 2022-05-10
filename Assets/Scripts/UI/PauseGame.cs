using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PauseGame : MonoBehaviour
{
	public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public Player_Actions playerControls;
	private InputAction pauseGame;
	EventSystem eventSystem;
	private void Start() {
		eventSystem = EventSystem.current;
	}
	private void Awake() {
		playerControls = new Player_Actions();	
	}

	private void OnEnable() {
		playerControls.Enable();
		//Jump Input
		pauseGame = playerControls.Player.PauseGame;
		pauseGame.Enable();
		pauseGame.performed += PauseAction;
	}

	private void OnDisable() {
		playerControls.Disable();
		pauseGame.Disable();
	}

	private void PauseAction(InputAction.CallbackContext context){
		Debug.Log("Player Paused Game");
		PauseToggle();
	}

	public void PauseToggle(){
		//Code turns on Inventory
		PauseMenuUI.SetActive(!PauseMenuUI.activeSelf);
		if (Time.timeScale == 1.0f) {
			GameIsPaused = true;
			Time.timeScale = 0.0f;
			eventSystem.SetSelectedGameObject(PauseMenuUI.transform.GetChild(1).gameObject);  
		}else{
			GameIsPaused = false;
			Time.timeScale = 1.0f;  
		}	
	}
}
