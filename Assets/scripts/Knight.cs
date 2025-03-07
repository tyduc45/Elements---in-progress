using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(touchingDirection))]
public class Knight : MonoBehaviour
{
    Rigidbody2D            rb;
    Animator               animator;
    touchingDirection      thisEnemy;


    public detectZone attackZone;

    [SerializeField]
    private float walkSpeed = 3f;

    private bool _seePlayer = false;
    public bool seePlayer
    {
        get
        {
            return _seePlayer;
        }
        set
        {
            _seePlayer = value;
            animator.SetBool("seePlayer", value);
        }
    }


    public enum WalkDirection { Right, Left }
    public Vector2 walkDirectionVector   = Vector2.right;
    private WalkDirection _walkDirection = WalkDirection.Right;
    public WalkDirection walkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(-1 * gameObject.transform.localScale.x, gameObject.transform.localScale.y);
                if (value == WalkDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
                else if (value == WalkDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
            }
            _walkDirection = value;
        }
    }
    

    
    

    private void Awake()
    {
        rb        = GetComponent<Rigidbody2D>();
        animator  = GetComponent<Animator>();
        thisEnemy = GetComponent<touchingDirection>();
    }
    private void FixedUpdate()
    {
        if (thisEnemy.isGround && thisEnemy.isWall)
        {
            filpDirection();
        }
        rb.linearVelocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.linearVelocityY);
    }

    private void filpDirection()
    {
        if(walkDirection == WalkDirection.Left)
        {
            walkDirection = WalkDirection.Right;
        }
        else if(walkDirection == WalkDirection.Right)
        {
            walkDirection = WalkDirection.Left;
        }
        else
        {
            Debug.Log("wrong!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        seePlayer = attackZone.targetList.Count > 0;
    }
}
