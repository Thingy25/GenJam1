using UnityEngine;

public class SoundSceneManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private AudioSource backgroundMusicSource; // Para m�sica de fondo
    [SerializeField] private AudioSource sfxSource;             // Para efectos de sonido de escena

    [Header("Clips de Sonido de Escena")]
    [SerializeField] private AudioClip backgroundMusicClip;
    [SerializeField] private AudioClip environmentSound; // Por ejemplo, el canto de un p�jaro
    [SerializeField] private AudioClip pickUpItemSound;  // Sonido al recoger un objeto

    void Awake()
    {
        // Si no est�n asignados en el Inspector, intenta buscarlos en los hijos o en el propio GameObject
        // Aseg�rate de haber a�adido los dos AudioSource a este GameObject ("SoundManagerGlobal")
        if (backgroundMusicSource == null || sfxSource == null)
        {
            AudioSource[] sources = GetComponents<AudioSource>();
            if (sources.Length >= 2)
            {
                // Asume el primer AudioSource es para m�sica, el segundo para SFX
                // O puedes darles nombres espec�ficos y buscar por nombre si lo prefieres
                backgroundMusicSource = sources[0];
                sfxSource = sources[1];
            }
            else
            {
                Debug.LogError("SoundSceneManager: Se requieren al menos dos AudioSource en este GameObject (uno para m�sica, uno para SFX).");
            }
        }

        // Configura la m�sica de fondo
        if (backgroundMusicSource != null && backgroundMusicClip != null)
        {
            backgroundMusicSource.clip = backgroundMusicClip;
            backgroundMusicSource.loop = true; // Para que se repita la m�sica
            backgroundMusicSource.playOnAwake = false; // Queremos controlarlo por c�digo
        }
    }

    void Start()
    {
        PlayBackgroundMusic();
        // Puedes llamar a otros sonidos de ambiente aqu� si quieres que empiecen con la escena
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

    // Agrega m�s m�todos p�blicos para reproducir otros sonidos globales que necesites
}