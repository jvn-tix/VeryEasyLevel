using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D)/*, typeof(GroundChecker)*/)]
public class CharacterJump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    private Rigidbody2D rb;

    public event Action OnJumped;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed /*&& groundChecker.IsGrounded()*/)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            OnJumped?.Invoke();
        }
    }       
}
