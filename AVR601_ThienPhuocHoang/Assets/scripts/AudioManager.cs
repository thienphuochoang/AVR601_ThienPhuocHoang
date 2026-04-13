using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip shootSound;
    public AudioClip explosionSound;
    public AudioClip gameOverSound;

    void Awake()
    {
        Instance = this;
    }

    public void PlayShoot()
    {
        sfxSource.PlayOneShot(shootSound);
    }

    public void PlayExplosion()
    {
        sfxSource.PlayOneShot(explosionSound);
    }

    public void PlayGameOver()
    {
        sfxSource.PlayOneShot(gameOverSound);
    }
}