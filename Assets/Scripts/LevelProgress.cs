using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour
{
    [SerializeField] private CharacterDeath player;
    [SerializeField] private float loadDelay = 1.5f; 

    private void OnEnable() => player.OnDied += HandleDied;
    private void OnDisable() => player.OnDied -= HandleDied;

    private void HandleDied()
    {
        Invoke(nameof(LoadNext), loadDelay);
    }

    private void LoadNext()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;

        if (next < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(next);
        else
            SceneManager.LoadScene(0);
    }
}