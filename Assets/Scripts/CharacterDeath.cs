using System;
using UnityEngine;

public class CharacterDeath : MonoBehaviour
{
    private Animator animator;
    public event Action OnDied;
    public bool IsDead { get; private set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Die()
    {
        if (IsDead) return;
        IsDead = true;

        OnDied?.Invoke();

        animator.SetTrigger("Die");

        GetComponent<CharacterMovement>().enabled = false;
        GetComponent<CharacterJump>().enabled = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}