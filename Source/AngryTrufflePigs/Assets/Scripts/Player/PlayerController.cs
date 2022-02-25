using UnityEngine;

enum PlayerMovementState
{
    Idle,
    Walking
}

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private Vector2 move;
    [SerializeField] private float speed = 5.0f;
    private PlayerMovementState state;

    [Header("Components")]
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();    
    }

    // Update is called once per frame
    private void Update()
    {
        ToggleAnimator();
        FlipSprite();
        GetInputs();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(move.x * speed, move.y * speed);
    }

    private void ToggleAnimator()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.01f || Mathf.Abs(rb.velocity.y) > 0.01f)
        {
            state = PlayerMovementState.Walking;
        }
        else
        {
            state = PlayerMovementState.Idle;
            
        }

        animator.SetInteger("MoveState", (int)state);
    }

    private void FlipSprite()
    {
        if (state == PlayerMovementState.Walking && rb.velocity.x > 0)
        {
            sprite.flipX = false;
        }

        if (state == PlayerMovementState.Walking && rb.velocity.x < 0)
        {
            sprite.flipX = true;
        }
    }

    private void GetInputs()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        move = new Vector2(x, y).normalized;
    }
}
