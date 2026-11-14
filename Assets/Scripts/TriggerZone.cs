using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private UnityEvent onPlayerEnter;
    private bool used;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (used) return;

        if (other.TryGetComponent(out CharacterDeath _))
        {
            used = true;
            onPlayerEnter.Invoke();
        }
    }
}