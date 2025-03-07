using UnityEngine;

public class Damageble : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private int _maxHealth = 100;

    private bool _isAlive  = true;
    private bool _isInvincible = false;

    private float lastHitTime = 0f;
    public float hitInterval = 0.25f;

    public int maxHealth
    {
        get { return _maxHealth; }
        set 
        {
            _maxHealth = value;
            if (_maxHealth <= 0)
            {
                isAlive = false;
            }
        }
    }


    public bool isAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            animator.SetBool("isAlive", value);
            Debug.Log("this character is dead");
        }
    }

    public bool isInvincible
    {
        get { return _isInvincible; }
        set
        {
            _isInvincible = value;
            animator.SetBool("isInvincible", value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isInvincible && Time.time - lastHitTime >= hitInterval)
        {
            isInvincible = false;
            lastHitTime = Time.time;
        }
        OnHit(10);
    }
    
    private void OnHit(int damage)
    {
        if(isAlive && !isInvincible)
        {
            maxHealth -= damage;
            isInvincible = true;
        }
    }
}
