using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string firstLevelName = "Level1";

    public void PlayGame()
    {
        SceneManager.LoadScene(firstLevelName);
    }
}