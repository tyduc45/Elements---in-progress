using UnityEngine;

public class touchingDirection : MonoBehaviour
{

    public  ContactFilter2D     contactFilter;
    private CapsuleCollider2D   capsuleCollider;
    private Animator            animator;


    //contact detect distance
    private float groundDistance = 0.05f;  
    private float wallDistance   = 0.2f;  
    private float ceilDistance   = 0.05f;  

    [SerializeField]
    private int _dashCount   = 2;
    [SerializeField]
    private int _jumpCount   = 1;
    [SerializeField]
    private bool _isGround   = false;
    [SerializeField]
    private bool _isWall     = false;
    [SerializeField]
    private bool _isCeil     = false;

    RaycastHit2D[] groundHit = new RaycastHit2D[5];
    RaycastHit2D[] wallHit   = new RaycastHit2D[5];
    RaycastHit2D[] ceilHit   = new RaycastHit2D[5];

    private Vector2 horizontalDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

   
    public int dashCount
    {
        get { return _dashCount; }
        set { _dashCount = value; }
    }

    
    public int jumpCount
    {
        get { return _jumpCount; }
        set { _jumpCount = value; }
    }

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
    
    public bool isCeil
    {
        get
        {
            return _isCeil;
        }
        set
        {
            _isCeil = value;
            animator.SetBool("isCeil", value);
        }
    }

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator        = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        // check is character is at ground
        isGround = capsuleCollider.Cast(Vector2.down, contactFilter, groundHit, groundDistance) > 0;
        isWall   = capsuleCollider.Cast(horizontalDirection,contactFilter,wallHit,wallDistance) > 0;
        isCeil   = capsuleCollider.Cast(Vector2.up, contactFilter, ceilHit, wallDistance) > 0;
    }
}
