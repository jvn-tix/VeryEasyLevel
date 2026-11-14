using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HoverAway : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform upPoint;
    [SerializeField] private float riseSpeed = 4f;
    [SerializeField] private float fallSpeed = 3f;
    [Header("Jarak (dua angka supaya tidak bolak-balik)")]
    [SerializeField] private float nearDistance = 3f; 
    [SerializeField] private float farDistance = 5f;  
    private Rigidbody2D rb;
    private Vector2 downPos;
    private bool isUp;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        downPos = rb.position;
    }

    private void FixedUpdate()
    {
        float distance = Vector2.Distance(rb.position, player.position);

        if (distance < nearDistance) isUp = true;
        else if (distance > farDistance) isUp = false;

        Vector2 target = isUp ? (Vector2)upPoint.position : downPos;
        float speed = isUp ? riseSpeed : fallSpeed;

        rb.MovePosition(Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime));
    }
}
