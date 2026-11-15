using UnityEngine;
using UnityEngine.InputSystem;
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movementInput;
    public bool isFacingRight = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movementInput.x * moveSpeed, rb.linearVelocity.y);

        HandleFlip();

        animator.SetFloat("Speed", Mathf.Abs(movementInput.x));
    }

    void HandleFlip()
    {
        if(movementInput.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (movementInput.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }
}
