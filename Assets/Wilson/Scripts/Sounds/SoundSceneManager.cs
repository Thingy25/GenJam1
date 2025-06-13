using UnityEngine;

public class SoundSceneManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private AudioSource backgroundMusicSource; // Para música de fondo
    [SerializeField] private AudioSource sfxSource;             // Para efectos de sonido de escena

    [Header("Clips de Sonido de Escena")]
    [SerializeField] private AudioClip backgroundMusicClip;
    [SerializeField] private AudioClip environmentSound; // Por ejemplo, el canto de un pájaro
    [SerializeField] private AudioClip pickUpItemSound;  // Sonido al recoger un objeto

    void Awake()
    {
        // Si no están asignados en el Inspector, intenta buscarlos en los hijos o en el propio GameObject
        // Asegúrate de haber añadido los dos AudioSource a este GameObject ("SoundManagerGlobal")
        if (backgroundMusicSource == null || sfxSource == null)
        {
            AudioSource[] sources = GetComponents<AudioSource>();
            if (sources.Length >= 2)
            {
                // Asume el primer AudioSource es para música, el segundo para SFX
                // O puedes darles nombres específicos y buscar por nombre si lo prefieres
                backgroundMusicSource = sources[0];
                sfxSource = sources[1];
            }
            else
            {
                Debug.LogError("SoundSceneManager: Se requieren al menos dos AudioSource en este GameObject (uno para música, uno para SFX).");
            }
        }

        // Configura la música de fondo
        if (backgroundMusicSource != null && backgroundMusicClip != null)
        {
            backgroundMusicSource.clip = backgroundMusicClip;
            backgroundMusicSource.loop = true; // Para que se repita la música
            backgroundMusicSource.playOnAwake = false; // Queremos controlarlo por código
        }
    }

    void Start()
    {
        PlayBackgroundMusic();
        // Puedes llamar a otros sonidos de ambiente aquí si quieres que empiecen con la escena
        // PlayEnvironmentSound();
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusicSource != null && backgroundMusicClip != null && !backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        if (backgroundMusicSource != null && backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Stop();
        }
    }

    public void PlayEnvironmentSound()
    {
        if (sfxSource != null && environmentSound != null)
        {
            sfxSource.PlayOneShot(environmentSound);
        }
    }

    public void PlayPickUpItemSound()
    {
        if (sfxSource != null && pickUpItemSound != null)
        {
            sfxSource.PlayOneShot(pickUpItemSound);
        }
    }

    // Agrega más métodos públicos para reproducir otros sonidos globales que necesites
}