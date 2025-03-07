using UnityEngine;

public class touchingDirectionEnemy : MonoBehaviour
{

    ContactFilter2D   contactFilter;
    CapsuleCollider2D capsuleCollider;
    Animator          animator;

    //contact detect distance
    private float groundDistance = 0.05f;
    private float wallDistance   = 0.2f;

    [SerializeField]
    private bool _isGround = false;
    [SerializeField]
    private bool _isWall   = false;


    RaycastHit2D[] groundHit = new RaycastHit2D[5];
    RaycastHit2D[] wallHit   = new RaycastHit2D[5];


    private Vector2 horizontalDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;


   
    public bool isGround
    {
        get
        {
            return _isGround;
        }
        set
        {
            _isGround = value;
            animator.SetBool("isGround", value);
        }
    }

    public bool isWall
    {
        get
        {
            return _isWall;
        }
        set
        {
            _isWall = value;
            animator.SetBool("isWall", value);
        }
    }

  

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator    = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        
        // check is character is at ground
        isGround = capsuleCollider.Cast(Vector2.down, contactFilter, groundHit, groundDistance) > 0;
        isWall   = capsuleCollider.Cast(horizontalDirection, contactFilter, wallHit, wallDistance) > 0;
    }
}
