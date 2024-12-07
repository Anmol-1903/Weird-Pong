using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    bool musicPlaying = false;

    [SerializeField] private AudioClip ui, ready;
    [SerializeField] private AudioClip playerScore, enemyScore;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayMusic();
    }

    void PlayMusic()
    {
        musicPlaying = true;
        musicSource.volume = 0;
        musicSource.Play();
    }

    private void Update()
    {
        if(musicPlaying)
        {
            musicSource.volume = Mathf.Lerp(musicSource.volume, 0.75f, Time.deltaTime / 2);
        }
    }

    public void PlayButtonClick()
    {
        sfxSource.PlayOneShot(ui);
    }
    public void PlayerReadySFX()
    {
        sfxSource.PlayOneShot(ready);
    }
    public void PlayerScoreSFX()
    {
        sfxSource.PlayOneShot(playerScore);
    }
    public void EnemyScoreSFX()
    {
        sfxSource.PlayOneShot(enemyScore);
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

}