using UnityEngine;

public class ObjectDragSound : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private AudioSource objectAudioSource;
    [SerializeField] private AudioClip dragSoundClip;

    [Header("Configuraci�n de Sonido")]
    [Tooltip("Velocidad horizontal m�nima para que el sonido de arrastre comience a sonar.")]
    [SerializeField] private float minSpeedForSound = 0.1f;
    [Tooltip("La velocidad a la que el volumen del sonido de arrastre alcanza su m�ximo.")]
    [SerializeField] private float maxVolumeSpeed = 2.0f;

    private Rigidbody rb;
    private bool isSoundPlaying = false; // Para controlar si el sonido ya est� sonando en bucle
    private bool isOnGround = false;     // Para saber si el objeto est� en contacto con el suelo

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError($"ObjectDragSound en {gameObject.name}: No se encontr� un Rigidbody. El movimiento no funcionar� correctamente.");
            this.enabled = false;
            return;
        }

        if (objectAudioSource == null)
        {
            objectAudioSource = GetComponent<AudioSource>();
        }
        if (objectAudioSource == null)
        {
            Debug.LogError($"ObjectDragSound en {gameObject.name}: No se encontr� un AudioSource. Aseg�rate de a�adir uno.");
            this.enabled = false;
            return;
        }

        objectAudioSource.playOnAwake = false;
        objectAudioSource.loop = true; // Lo configuramos para loop aqu�, pero lo controlamos con Play/Stop

        if (dragSoundClip == null)
        {
            Debug.LogWarning($"ObjectDragSound en {gameObject.name}: No se ha asignado un AudioClip para el sonido de arrastre.");
        }
    }

    void FixedUpdate()
    {
        if (dragSoundClip == null || !objectAudioSource.enabled) return;

        // Calcula la velocidad horizontal (ignorando el componente Y, que es salto/ca�da)
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        float currentHorizontalSpeed = horizontalVelocity.magnitude;

        // Condici�n para que el sonido se reproduzca: en el suelo Y movi�ndose lo suficientemente r�pido
        bool shouldPlaySound = isOnGround && currentHorizontalSpeed >= minSpeedForSound;

        if (shouldPlaySound && !isSoundPlaying)
        {
            // Iniciar el sonido si debe sonar y a�n no lo hace
            objectAudioSource.clip = dragSoundClip;
            objectAudioSource.Play(); // Inicia el bucle
            isSoundPlaying = true;
            Debug.Log($"Objeto {gameObject.name}: Sonido de arrastre iniciado. Velocidad: {currentHorizontalSpeed}");
        }
        else if (!shouldPlaySound && isSoundPlaying)
        {
            // Detener el sonido si ya no debe sonar y a�n lo hace
            objectAudioSource.Stop();
            isSoundPlaying = false;
            Debug.Log($"Objeto {gameObject.name}: Sonido de arrastre detenido. Velocidad: {currentHorizontalSpeed}");
        }

        // Ajustar el volumen del sonido de arrastre basado en la velocidad
        if (isSoundPlaying)
        {
            // Calcula el volumen entre 0 y 1, proporcional a la velocidad
            objectAudioSource.volume = Mathf.InverseLerp(0, maxVolumeSpeed, currentHorizontalSpeed);
        }
    }

    // M�todos para detectar el contacto con el suelo
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            Debug.Log($"Objeto {gameObject.name}: Entr� en contacto con el suelo.");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
            // Asegurarse de detener el sonido si deja el suelo
            if (isSoundPlaying)
            {
                objectAudioSource.Stop();
                isSoundPlaying = false;
                Debug.Log($"Objeto {gameObject.name}: Sali� del suelo. Sonido de arrastre detenido.");
            }
        }
    }
}