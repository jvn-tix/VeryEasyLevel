using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowPlatform : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private float reactionDelay = 0.4f;
    [SerializeField] private float deadZone = 0.3f;

    [Header("Batas gerak")]
    [SerializeField] private float minX = -6f;
    [SerializeField] private float maxX = 6f;

    private Rigidbody2D rb;
    private float targetX;
    private float timer;
    private bool active = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetX = rb.position.x;
    }

    private void Start()
    {
        if (player.TryGetComponent(out CharacterDeath death))
            death.OnDied += () => active = false;
    }

    public void Activate()
    {
        active = true;
        targetX = Mathf.Clamp(player.position.x, minX, maxX);
    }

    public void Deactivate()
    {
        active = false;
    }

    private void FixedUpdate()
    {
        bool playerAbove = player.position.y > rb.position.y;

        if (active && playerAbove)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= reactionDelay)
            {
                timer = 0f;
                targetX = Mathf.Clamp(player.position.x, minX, maxX);
            }
        }

        float diff = targetX - rb.position.x;
        if (Mathf.Abs(diff) < deadZone)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = new Vector2(Mathf.Sign(diff) * speed, 0f);
    }
}