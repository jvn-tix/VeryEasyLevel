using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private bool triggerOnce = true;
    [SerializeField] private UnityEvent onPlayerEnter;
    [SerializeField] private UnityEvent onPlayerExit;
    private bool used;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerOnce && used) return;

        if (other.TryGetComponent(out CharacterDeath _))
        {
            UnityEngine.Debug.Log("Player entered trigger zone.");
            used = true;
            onPlayerEnter.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (triggerOnce && used) return;
        if (other.TryGetComponent(out CharacterDeath _))
        {
            used = true;
            onPlayerExit.Invoke();
        }
    }
}