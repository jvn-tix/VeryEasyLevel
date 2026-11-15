using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float checkRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, checkRadius, groundLayer) != null;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheckPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, checkRadius);
    }
}