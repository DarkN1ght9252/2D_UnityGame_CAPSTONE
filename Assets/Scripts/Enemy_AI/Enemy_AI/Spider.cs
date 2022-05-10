using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public float jumpForce = .5f;
    public float speed = 1;
    public float extraLength = 1f;
    public float lineOfSightHeight = 1f;
    public float boxLength = 1f;
    public float boxHeight = 1f;
    public float jumpingCooldown = 3;
    private float jumpingCoolDownCounter = 0;
    
    private bool coolingdown = false;
    private bool isGrounded = true;
    private Vector3 raycastHeight;
    private Vector2 boxBoundsAdditional;
    private BoxCollider2D _box;
    private Rigidbody2D _body;
    private Animator _anim;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private LayerMask groundLayermask;

    private bool IsGrounded(){
		float extraHeight = .02f;
        Vector3 addedRayCastHeight = new Vector3(_box.bounds.min.x,_box.bounds.min.y ,_box.bounds.min.x);
		RaycastHit2D raycastHit = Physics2D.BoxCast(_box.bounds.center, _box.bounds.size ,0f, Vector2.down, _box.bounds.extents.y + extraHeight , groundLayermask);
		Color rayColor;
		if(raycastHit.collider != null){
			rayColor = Color.green;
		}else{
			rayColor = Color.red;
		}
		Debug.Log(raycastHit.collider);
		// Debug.DrawRay(_box.bounds.center,Vector2.down*(_box.bounds.extents.y + extraHeight), rayColor);
		return raycastHit.collider != null;
	}

    // Start is called before the first frame update
    void Start()
    {
        _box = GetComponent<BoxCollider2D>();
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        //increases Height of boxCast position
       
        //Sets size of BoxCast
        boxBoundsAdditional = _box.bounds.size;
        boxBoundsAdditional.y += boxHeight;

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = IsGrounded();
        raycastHeight = _box.bounds.center;
        raycastHeight.x -= .84f;
        RaycastHit2D fraycastHit = Physics2D.BoxCast(raycastHeight,boxBoundsAdditional,0f,Vector2.left, _box.bounds.extents.x + extraLength,playerLayerMask);
        raycastHeight.x += 3.5f;
        RaycastHit2D braycastHit = Physics2D.BoxCast(raycastHeight,boxBoundsAdditional,0f,Vector2.right, _box.bounds.extents.x + extraLength,playerLayerMask);
        
        Color frayColor;
        Color brayColor;

        if (fraycastHit.collider != null)
        {
            _body.velocity = new Vector2(-speed,_body.velocity.y);
            if (isGrounded && !coolingdown)
             {
                _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpingCoolDownCounter = jumpingCooldown;
                coolingdown = true;
             }
            frayColor = Color.green;
        }else {
            frayColor = Color.red;
        }

        if (braycastHit.collider != null)
        {   
            _body.velocity = new Vector2(speed,_body.velocity.y);
            brayColor = Color.green;

            if (isGrounded && !coolingdown)
            {
                _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpingCoolDownCounter = jumpingCooldown;
                coolingdown = true;
            }
        }else{
             brayColor = Color.red;
        }

        if(isGrounded){
            _anim.SetFloat("jump", 1f);
        }else{
            _anim.SetFloat("jump", 0f);
        }

        //Flips sprite around based on movement
        if (!Mathf.Approximately(_body.velocity.x, 0)) {
			transform.localScale = new Vector3(Mathf.Sign(-_body.velocity.x), 1, 1);
		}

        if(coolingdown){
            jumpingCoolDownCounter -= Time.deltaTime;
            if(jumpingCoolDownCounter <= 0){
                coolingdown = false;
            }
        }
        // Debug.Log("Velocity for animator : " + _body.velocity.y);
        raycastHeight.x -= 3.5f;
        Debug.DrawRay(raycastHeight,Vector2.left*(_box.bounds.extents.x+ extraLength), frayColor);
        raycastHeight.x += .83f;
        Debug.DrawRay(raycastHeight,Vector2.right*(_box.bounds.extents.x + extraLength), brayColor);
    }

}
