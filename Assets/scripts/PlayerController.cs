using System;
using System.Collections;
using System.Threading;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    touchingDirection character;
    Vector2           moveInput;
    Rigidbody2D       rb;
    Animator          animator;

    [SerializeField]
    private float _yVelocity        = 0f;       // record y-velocity
    [SerializeField]
    private bool _isRunning         = false;    //  backing store
    [SerializeField]
    private bool _isDashing         = false;

    private bool _isRunningRight    = true;
    private float _lastdash         = -10f;     // record the last dash time
    private float _dashCalm         = 0.35f;
    private bool _heavyAttack       = false;
    

    public float dashSpeed          = 14f;
    public float dashDuration       = 0.2f;     
    public float walkspeed          = 7f;
    private float attackImpulse     = 7f;
    private float jumpImpulse       = 10f;
    
    public float holdInsist         = 0;        //  botton insist time

    private float attackDuration    = 0;        // heavy attack duration

    private float holdUpperBold     = 1.0f;     // upperbold of heavy attack 

    private int originalLayer;                  // original layer imformation


    private void Awake()
    {
        originalLayer = gameObject.layer;       // get current layer imformation

        rb            = GetComponent<Rigidbody2D>();
        animator      = GetComponent<Animator>();
        character     = GetComponent<touchingDirection>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // attack logic
        if(Input.GetMouseButton(0))
        {
            holdInsist += Time.deltaTime;                                      // long-hold detect
        }
        if (Input.GetMouseButtonUp(0))
        {
            attackDuration = holdInsist >= holdUpperBold ? holdUpperBold : holdInsist;
            heavyAttack    = true;
            StartCoroutine(HeavyAttack());
        }
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
        }
    }

    // handle physics-related operation.
    private void FixedUpdate()
    {
      // get y-velocity
      yVelocity = rb.linearVelocityY;

      if(!isRunningRight)
        {
            //dash from idle when facing left
            if(isDashing && !isRunning)
            {
                rb.linearVelocity = new Vector2(-1 * getSpeed(), rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(moveInput.x * getSpeed(), rb.linearVelocity.y);
            }
        }
      else
        {
            //dash from idle when facing right
            if (isDashing && !isRunning)
            {
                rb.linearVelocity = new Vector2(getSpeed(), rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(moveInput.x * getSpeed(), rb.linearVelocity.y);
            }
        }
        // reset jumpCount
        if (character.isGround) { character.jumpCount = 1; }
    }

    


    //change direction and make filp
    void changeDirection(Vector2 moveInput)
    {
       if(moveInput.x > 0 && !isRunningRight)
       {
           isRunningRight = true;
       }
       else if(moveInput.x < 0 && isRunningRight)
       {
            isRunningRight = false;
        }
    }

    // get current speed
    float getSpeed()
    {
        if(!character.isWall)
        {
            if(_isDashing)
            {
                return dashSpeed;
            }
            else
            {
                return walkspeed;
            }
        }
        else
        {
            return 0f;
        }
    }


    // let character move and filp
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        isRunning = moveInput != Vector2.zero;

        changeDirection(moveInput);
    }

    public void onJump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            // reset dashCount when air jump
            if (character.jumpCount > 0)
            {
                animator.SetTrigger("jump");
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpImpulse);

                character.jumpCount--;
                character.dashCount = 1; // give one more chance to dash
            }
        }
    }

    // play dash animation
    public void onDash(InputAction.CallbackContext context)
    {
        if(character.isGround)
        {
            // 检查是否处于冷却状态
            if (Time.time - lastdash < dashCalm)
            {
                return; // 冷却时间未到，忽略输入
            }

            if (context.started)
            {
                StartCoroutine(Dash()); // 启动冲刺协程
                lastdash = Time.time;   // 记录本次冲刺时间
            }
        }
        else
        {
            if(character.dashCount > 0) // allow airdash once
            {
                if (context.started)
                {
                    StartCoroutine(Dash()); // 启动冲刺协程
                }
                character.dashCount--;
            }
            else
            {
                return;
            }
        }
    }
    private IEnumerator Dash()
    {
        gameObject.layer = LayerMask.NameToLayer("PlayerDashing");  // change layer of palyer

        isDashing = true;
        yield return new WaitForSeconds(dashDuration);              // keep dashing for a periond of time
        isDashing = false;                                          // stop dashing automatcally

        gameObject.layer = originalLayer;                           // go back to the layer where it use to be              
    }

    private IEnumerator HeavyAttack()
    {
        yield return new WaitForSeconds(attackDuration);                // start heavy attack loop
        heavyAttack = false;                                            // stop heavy attack automatically
        holdInsist  = 0;
    }


    // variables
    float lastdash
    {
        get { return _lastdash; }
        set { _lastdash = value; }
    }
    // set dash calm time

    float dashCalm
    {
        get { return _dashCalm; }
        set { _dashCalm = value; }
    }
    bool isRunningRight
    {
        get
        {
            return _isRunningRight;
        }
        set
        {
            _isRunningRight = value;
            transform.localScale *= new Vector2(-1, 1);
        }
    }

    public bool isRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool("isRunning", value);
        }
    }

    public bool isDashing
    {
        get
        {
            return _isDashing;
        }
        set
        {
            _isDashing = value;
            animator.SetBool("isDashing", value);
        }
    }

    public float yVelocity
    {
        get
        {
            return _yVelocity;
        }
        set
        {
            _yVelocity = value;
            animator.SetFloat("yVelocity", value);
        }
    }

    public bool heavyAttack
    {
        get
        {
            return _heavyAttack;
        }
        set
        {
            _heavyAttack = value;
            animator.SetBool("heavyAttack", value);
        }
    }

}
