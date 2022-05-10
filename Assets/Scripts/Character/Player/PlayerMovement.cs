using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Transform))]

public class PlayerMovement : Player
{
	// Physics Variables
    public float speed = 5.0f;
	public float Max_Speed = 10.0f;
    public float jumpForce = 12.0f;

    public float gravityForce = 45.0f;
	
	private bool hasDoubleJumped = false;
    public float minHeightForDeath;

	//Animation Booleans
	private bool isgrounded = false;
	private bool  isCrouched = false;
	
	// Abilities Unlocks
	private bool doubleJumpUnlocked = false;
	//Movement Lock
	// private bool lockMovement = false;
	//Combat Variables
	public Transform gunBarrel;
	public Transform crouchingGunBarrel;


    private Animator _anim;
    private Rigidbody2D _body;
    private BoxCollider2D _box;
	private BoxCollider2D _box_crouch;
	[SerializeField] private LayerMask groundLayerMask;
	[SerializeField] private LayerMask enemyMask;

	//Player inputs
	public Player_Actions playerControls;
	private InputAction move;
	private InputAction jump;
	private InputAction crouch;
	private InputAction releaseCrouch;
	Vector2 inputMovement = Vector2.zero;
	
	private void Awake() {
		playerControls = new Player_Actions();	}

	private void OnEnable() {
		playerControls.Enable();
		//Movement Input
		move = playerControls.Player.Move;
		move.Enable();
		//Jump Input
		jump = playerControls.Player.Jump;
		jump.Enable();
		jump.performed += Jump;
		//Crouch Input
		crouch = playerControls.Player.Crouch;
		crouch.Enable();
		crouch.performed += Crouch;
		releaseCrouch = playerControls.Player.ReleaseCrouch;
		releaseCrouch.Enable();
		releaseCrouch.performed += ReleaseCrouch;

	}

	private void OnDisable() {
		playerControls.Disable();
		move.Disable();
		jump.Disable();
		crouch.Disable();
	}

	private void Jump(InputAction.CallbackContext context){
		Debug.Log("Player Jump");

		//Code implements Double Jump in Else if
		if (isgrounded) {
			hasDoubleJumped = false;
			_body.gravityScale = gravityForce;
			_body.velocity = new Vector2(_body.velocity.x,0);
			_body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}else if(!isgrounded && !hasDoubleJumped && doubleJumpUnlocked){
			_body.velocity = new Vector2(_body.velocity.x,0);
			_body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			hasDoubleJumped = true;
		}
	}
	private void Crouch(InputAction.CallbackContext context){
		Debug.Log("Player Crouched");
		//Crouch Code
		if(isgrounded){
			isCrouched = true;
			_anim.SetBool("crouched", true);
		}
	}

	private void ReleaseCrouch(InputAction.CallbackContext context){
		Debug.Log("Player unCrouched");
		isCrouched = false;
		_anim.SetBool("crouched", false);
		
	}

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _box = GetComponent<BoxCollider2D>();
		_box_crouch = GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();
    }

	//Method for checking if player is on the Ground
	private bool IsGrounded(){
		float extraHeight = .02f;
		RaycastHit2D raycastHit = Physics2D.BoxCast(_box.bounds.center, _box.bounds.size ,0f, Vector2.down, _box.bounds.extents.y + extraHeight , groundLayerMask);
		isgrounded = false;
		Color rayColor;
		if(raycastHit.collider != null){
			isgrounded = true;
			rayColor = Color.green;
		}else{
			rayColor = Color.red;
		}
		// Debug.Log(raycastHit.collider);
		Debug.DrawRay(_box.bounds.center,Vector2.down*(_box.bounds.extents.y + extraHeight), rayColor);
		return raycastHit.collider != null;
	}

    // Update is called once per frame
    void Update()
    {
		if(!isCrouched){
			//Grabs Input
			inputMovement = playerControls.Player.Move.ReadValue<Vector2>();
			//Checks if grounded
			IsGrounded();
			
			float deltaX;
			//Horizontal Movement Checks Shift is Held to then Run
			deltaX = inputMovement.x * speed;
			
			//creates new vector 2 of current movement applied to player
			Vector2 movement = new Vector2(deltaX, _body.velocity.y);
			_body.velocity = movement;
			
			Vector3 max = _box.bounds.max;
			Vector3 min = _box.bounds.min;
			Vector2 corner1 = new Vector2(max.x, min.y+.1f);
			Vector2 corner2 = new Vector2(min.x, min.y -.2f);
			Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
			//Adjusts how fast a player falls back down
			_body.gravityScale = (isgrounded && Mathf.Approximately(deltaX, 0)) ? 0 : gravityForce;
			
			
			//Moves Player along with a moving platform 
			MovingPlatform platform = null;

			if (hit != null) {
				platform = hit.GetComponent<MovingPlatform>();
			}

			if (platform != null) {
				transform.parent = platform.transform;
			} else {
				transform.parent = null;
			}

			_anim.SetFloat("speed", Mathf.Abs(deltaX));

			Vector3 pScale = Vector3.one;
			if (platform != null) {
				pScale = platform.transform.localScale;
			}

			if (!Mathf.Approximately(deltaX, 0)) {
				transform.localScale = new Vector3(Mathf.Sign(deltaX)/ pScale.x, 1/pScale.y, 1);
			}
			//Sets a Value that if player reaches will kill and reset the player
			if(transform.position.y < minHeightForDeath){
				transform.position = new Vector3(-12,0,-.1f);
			}
		}
		
		
    }

	public Transform GetTransform(){
		return transform;
	}
	
	public override void OnCollisionEnter2D(Collision2D other) {
		base.OnCollisionEnter2D(other);
       	if(other.gameObject.layer == 8){
			hasDoubleJumped = false;
		}   
    }

	public void SetDoubleJumpAbility(bool enableDoubleJump){
		doubleJumpUnlocked = enableDoubleJump;
	}
	public bool getIsCrouched(){
		return isCrouched;
	}
}
