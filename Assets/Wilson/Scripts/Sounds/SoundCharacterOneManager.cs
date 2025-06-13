using UnityEngine;
using System;

public class SoundCharacterOneManager : MonoBehaviour
{
    [Header("Referencias")]
    // Referencia al controlador del personaje uno (deber�a estar en el mismo GameObject)
    [SerializeField] private CharacterOneControllerWilson characterOneController;
    // Referencia al AudioSource compartido en la c�mara
    private AudioSource playerAudioSource;

    [Header("Clips de Sonido")]
    [SerializeField] private AudioClip jumpSound; // Sonido al saltar
    [SerializeField] private AudioClip slideSound;

    void Awake()
    {
        // Aseg�rate de que el controlador est� asignado o lo busca en este mismo GameObject
        if (characterOneController == null)
        {
            characterOneController = GetComponent<CharacterOneControllerWilson>();
        }

        // --- Obtiene el AudioSource de la c�mara principal ---
        playerAudioSource = Camera.main.GetComponent<AudioSource>();

        if (playerAudioSource == null)
        {
            Debug.LogError("SoundCharacterOneManager: No se encontr� un AudioSource en la C�mara Principal. Aseg�rate de a�adir uno.");
        }


    }

    void OnEnable()
    {
        // Suscribirse al evento de salto solo si el controlador y la fuente de audio existen
        if (characterOneController != null && playerAudioSource != null)
        {
            characterOneController.OnJumpStarted -= PlayJumpSound;
            characterOneController.OnJumpStarted += PlayJumpSound;

            characterOneController.OnSlideStarted -= StartSlideSound;
            characterOneController.OnSlideStarted += StartSlideSound;
            characterOneController.OnSlideStopped -= StopSlideSound;
            characterOneController.OnSlideStopped += StopSlideSound;
        }

    }

    void OnDisable()
    {
        // Desuscribirse del evento
        if (characterOneController != null && playerAudioSource != null)
        {
            characterOneController.OnJumpStarted -= PlayJumpSound; // Limpiar para evitar duplicados
            characterOneController.OnJumpStarted += PlayJumpSound;
            characterOneController.OnSlideStarted -= StartSlideSound;
            characterOneController.OnSlideStarted += StartSlideSound;
            characterOneController.OnSlideStopped -= StopSlideSound;
            characterOneController.OnSlideStopped += StopSlideSound;
            StopSlideSound();
        }
    }

    // Funci�n para reproducir el sonido de salto
    private void PlayJumpSound()
    {
        Debug.Log("PlayJumpSound llamado, intentando reproducir."); // <-- A�ade esta l�nea
        if (playerAudioSource != null && jumpSound != null)
        {
            playerAudioSource.PlayOneShot(jumpSound);
        }
        else
        {
            if (playerAudioSource == null) Debug.LogError("playerAudioSource es nulo en PlayJumpSound.");
            if (jumpSound == null) Debug.LogError("jumpSound es nulo en PlayJumpSound.");
        }
    }
    // --- NUEVO: Funciones para el sonido de deslizamiento ---
    private void StartSlideSound()
    {
        if (playerAudioSource != null && slideSound != null)
        {
            // Detener cualquier sonido actual del AudioSource y reproducir el de deslizamiento en bucle
            playerAudioSource.Stop();
            playerAudioSource.clip = slideSound;
            playerAudioSource.loop = true; // El sonido de deslizamiento suele ser en bucle
            playerAudioSource.Play();
    
        }

    }
    private void StopSlideSound()
    {
        if (playerAudioSource != null && playerAudioSource.isPlaying && playerAudioSource.clip == slideSound)
        {
            playerAudioSource.Stop();
            playerAudioSource.loop = false; // Importante para que no afecte otros sonidos
     
        }
    }
}