using UnityEngine;

public class CharacterSFX : MonoBehaviour
{
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip deathClip;

    private CharacterJump jump;
    private CharacterDeath death;

    private void Awake()
    {
        jump = GetComponent<CharacterJump>();
        death = GetComponent<CharacterDeath>();
    }

    private void OnEnable()
    {
        jump.OnJumped += PlayJump;
        death.OnDied += PlayDeath;
    }

    private void OnDisable()
    {
        jump.OnJumped -= PlayJump;
        death.OnDied -= PlayDeath;
    }

    private void PlayJump()
    {
        if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX(jumpClip);
    }

    private void PlayDeath()
    {
        if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX(deathClip);
    }
}