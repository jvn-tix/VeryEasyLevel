using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PathRunAway : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed = 5f;      
    [SerializeField] private float triggerDistance = 4f;

    private Rigidbody2D rb;
    private int currentIndex;
    private int targetIndex;
    private bool hasTarget;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.position = waypoints[0].position;
        currentIndex = 0;
    }

    private void FixedUpdate()
    {
        if (!hasTarget)
        {
            if (Vector2.Distance(rb.position, player.position) > triggerDistance) return;
            ChooseNextWaypoint();
        }

        Vector2 targetPos = waypoints[targetIndex].position;
        Vector2 newPos = Vector2.MoveTowards(rb.position, targetPos, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (newPos == targetPos)
        {
            currentIndex = targetIndex;
            hasTarget = false;
        }
    }

    private void ChooseNextWaypoint()
    {
        int n = waypoints.Length;
        int forward = (currentIndex + 1) % n;
        int backward = (currentIndex - 1 + n) % n;

        float distForward = Vector2.Distance(waypoints[forward].position, player.position);
        float distBackward = Vector2.Distance(waypoints[backward].position, player.position);

        targetIndex = distForward >= distBackward ? forward : backward;
        hasTarget = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (waypoints == null || waypoints.Length < 2) return;
        Gizmos.color = Color.yellow;
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] == null) continue;
            Transform next = waypoints[(i + 1) % waypoints.Length];
            if (next == null) continue;
            Gizmos.DrawLine(waypoints[i].position, next.position);
            Gizmos.DrawWireSphere(waypoints[i].position, 0.15f);
        }
    }
}