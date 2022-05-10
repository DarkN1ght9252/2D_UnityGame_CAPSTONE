using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Licker : MonoBehaviour
{
    public float speed = 1f;
    public float lineOfSightHeight = 1f;
    public float boxLength = 1f;
    public float boxHeight = 1f;
     public float extraLength = 1f;
    private BoxCollider2D _box;
    private Rigidbody2D _body;
    private Animator _anim;
    [SerializeField] private LayerMask playerLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        _box = GetComponent<BoxCollider2D>();
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 raycastHeight = _box.bounds.center;
        raycastHeight.y += lineOfSightHeight;
        Vector2 boxBoundsAdditional = _box.bounds.size;
        boxBoundsAdditional.x += boxLength;
        boxBoundsAdditional.y += boxHeight;
        //Sets (Front/back) BoxCast
        RaycastHit2D fraycastHit = Physics2D.BoxCast(raycastHeight,boxBoundsAdditional,0f,Vector2.left, _box.bounds.extents.x + extraLength,playerLayerMask);
        RaycastHit2D braycastHit = Physics2D.BoxCast(raycastHeight,boxBoundsAdditional,0f,Vector2.right, _box.bounds.extents.x + extraLength,playerLayerMask);
        //Declares Color of Rays
        Color frayColor;
        Color brayColor;

        if (fraycastHit.collider != null)
        {
           _body.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
            frayColor = Color.green;
        }else {
            frayColor = Color.green;
        }
        if (braycastHit.collider != null)
        {   
            brayColor = Color.green;
            _body.AddForce(Vector2.right * speed, ForceMode2D.Impulse);

        }else{
             brayColor = Color.red;
        }

        //Sets for animation
        // _anim.SetFloat("speed", _body.velocity.x);

        if (!Mathf.Approximately(_body.velocity.x, 0)) {
			transform.localScale = new Vector3(Mathf.Sign(-_body.velocity.x), 1, 1);
		}
        // Debug.Log("Velocity for animator : " + _body.velocity.y);
        Debug.DrawRay(raycastHeight,Vector2.left*(_box.bounds.extents.x + extraLength), frayColor);
        Debug.DrawRay(raycastHeight,Vector2.right*(_box.bounds.extents.x + extraLength), frayColor);
    }
}
