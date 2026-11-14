using UnityEngine;
using UnityEngine.Events;

public class FakeDoor : MonoBehaviour
{
    [System.Serializable]
    public class Hint
    {
        public int atAttempt = 2;   
        public UnityEvent onReached;
    }

    [SerializeField] private Hint[] hints;

    private int attempts;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out CharacterRespawn respawn)) return;

        attempts++;
        respawn.ResetToStart();

        foreach (var hint in hints)
        {
            if (hint.atAttempt == attempts)
                hint.onReached.Invoke();
        }
    }
}