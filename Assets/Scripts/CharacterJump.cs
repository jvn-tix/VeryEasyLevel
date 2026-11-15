using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(GroundChecker))]
public class CharacterJump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    private Rigidbody2D rb;
    private GroundChecker groundChecker;
    private CharacterDeath death;

    public event Action OnJumped;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundChecker = GetComponent<GroundChecker>();
        death = GetComponent<CharacterDeath>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (death.IsDead) return;
        
        if (context.performed && groundChecker.IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            OnJumped?.Invoke();
        }
    }       
}
