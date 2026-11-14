using System;
using UnityEngine;

public class CharacterDeath : MonoBehaviour
{
    public event Action OnDied;
    public bool IsDead { get; private set; }

    public void Die()
    {
        if (IsDead) return;
        IsDead = true;

        OnDied?.Invoke();

        GetComponent<CharacterMovement>().enabled = false;
        GetComponent<CharacterJump>().enabled = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }
}