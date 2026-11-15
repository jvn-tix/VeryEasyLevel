using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpikeHopper : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float triggerDistance = 2.5f;
    [SerializeField] private float landOffset = 4f;
    [SerializeField] private float arcHeight = 2f;
    [SerializeField] private float hopDuration = 0.5f;
    [SerializeField] private float cooldown = 0.3f;

    private Rigidbody2D rb;
    private Animator animator;
    private float groundY;
    private bool hopping;
    private float timer;
    private float cooldownTimer;
    private Vector2 from;
    private Vector2 to;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundY = rb.position.y;
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (animator != null) animator.SetBool("isJumping", hopping);
        if (hopping)
        {
            timer += Time.fixedDeltaTime;
            float t = Mathf.Clamp01(timer / hopDuration);

            Vector2 pos = Vector2.Lerp(from, to, t);
            pos.y += 4f * arcHeight * t * (1f - t);
            rb.MovePosition(pos);

            if (t >= 1f)
            {
                hopping = false;
                cooldownTimer = cooldown;
            }
            return;
        }

        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.fixedDeltaTime;
            return;
        }

        float dx = rb.position.x - player.position.x;
        if (Mathf.Abs(dx) < triggerDistance)
            StartHop(Mathf.Sign(dx));
    }

    private void StartHop(float spikeSide)
    {
        from = rb.position;
        to = new Vector2(player.position.x - spikeSide * landOffset, groundY);
        timer = 0f;
        hopping = true;
    }
}