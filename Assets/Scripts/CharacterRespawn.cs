using UnityEngine;

public class CharacterRespawn : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    public void ResetToStart()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = startPosition;
    }
}