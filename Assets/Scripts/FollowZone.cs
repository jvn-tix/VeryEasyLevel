using UnityEngine;
using UnityEngine.Events;

public class FollowZone : MonoBehaviour
{
    [SerializeField] private UnityEvent onPlayerEnter;
    [SerializeField] private UnityEvent onPlayerExit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out CharacterDeath _))
            onPlayerEnter.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out CharacterDeath _))
            onPlayerExit.Invoke();
    }
}