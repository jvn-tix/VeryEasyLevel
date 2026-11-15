using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformSaver : MonoBehaviour
{
    [SerializeField] private CharacterJump characterJump;
    [SerializeField] private Transform player;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private float speed = 6f;
    [SerializeField] private float reactionDelay = 0.1f;

    private Rigidbody2D rb;
    private Vector2 target;
    private float delayTimer = -1f;
    private bool armed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = rb.position;
    }

    private void OnEnable()
    {
        if (characterJump != null) characterJump.OnJumped += OnPlayerJumped;
    }

    private void OnDisable()
    {
        if (characterJump != null) characterJump.OnJumped -= OnPlayerJumped;
    }

    public void Arm() => armed = true;
    public void Disarm() => armed = false;

    private void OnPlayerJumped()
    {
        if (!armed) return;

        float middle = (leftPoint.position.x + rightPoint.position.x) / 2f;

        target = player.position.x > middle ? leftPoint.position : rightPoint.position;
        delayTimer = reactionDelay;
    }

    private void FixedUpdate()
    {
        if (delayTimer > 0f)
        {
            delayTimer -= Time.fixedDeltaTime;
            return;
        }

        rb.MovePosition(Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime));
    }
}