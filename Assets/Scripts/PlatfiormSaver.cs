using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformSaver : MonoBehaviour
{
    [SerializeField] private CharacterJump characterJump;
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float reactionDelay = 0.1f;

    private Rigidbody2D rb;
    private Vector2 leftPos;
    private Vector2 rightPos;
    private Vector2 target;
    private bool atLeft = true;
    private float delayTimer = -1f;
    private bool armed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        leftPos = rb.position;
        rightPos = leftPos + Vector2.right * moveDistance;
        target = leftPos;
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

        atLeft = !atLeft;
        target = atLeft ? leftPos : rightPos;
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