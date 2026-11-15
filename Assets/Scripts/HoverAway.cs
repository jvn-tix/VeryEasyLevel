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
    [SerializeField] private bool useDistance = true;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 downPos;
    private bool isUp;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        downPos = rb.position;
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (useDistance)
        {
            float distance = Vector2.Distance(downPos, player.position);

            if (distance < nearDistance) isUp = true;
            else if (distance > farDistance) isUp = false;
        }

        Vector2 target = isUp ? (Vector2)upPoint.position : downPos;
        float speed = isUp ? riseSpeed : fallSpeed;

        if (animator != null)
        animator.SetBool("isMoving", Vector2.Distance(rb.position, target) > 0.01f);

        rb.MovePosition(Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime));
    }

    public void SetUp(bool value)
    {
        isUp = value;
    }
}
