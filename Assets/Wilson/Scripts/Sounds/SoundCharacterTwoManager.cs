using UnityEngine;
using System;

public class SoundCharacterTwoManager : MonoBehaviour
{
    [Header("Referencias")]
    // Referencia al controlador del personaje dos
    [SerializeField] private CharacterTwoControllerWilson characterTwoController;
    // Referencia al AudioSource compartido en la cámara
    private AudioSource playerAudioSource;

    [Header("Clips de Sonido")]
    [SerializeField] private AudioClip flySound; // Sonido al volar

    void Awake()
    {
        // Asegúrate de que el controlador esté asignado o lo busca en este mismo GameObject
        if (characterTwoController == null)
        {
            characterTwoController = GetComponent<CharacterTwoControllerWilson>();
        }

        // --- Obtiene el AudioSource de la cámara principal ---
        playerAudioSource = Camera.main.GetComponent<AudioSource>();

        if (playerAudioSource == null)
        {
            Debug.LogError("SoundCharacterTwoManager: No se encontró un AudioSource en la Cámara Principal. Asegúrate de añadir uno.");
        }
    }

    void OnEnable()
    {
        // Suscribirse al evento de vuelo solo si el controlador y la fuente de audio existen
        if (characterTwoController != null && playerAudioSource != null)
        {
            characterTwoController.OnFlyStarted += PlayFlySound;
        }
    }

    void OnDisable()
    {
        // Desuscribirse del evento
        if (characterTwoController != null && playerAudioSource != null)
        {
            characterTwoController.OnFlyStarted -= PlayFlySound;
        }
    }

    // Función para reproducir el sonido de vuelo
    private void PlayFlySound()
    {
        Debug.Log("PlayFlySound llamado, intentando reproducir."); // <-- Añade esta línea
        if (playerAudioSource != null && flySound != null)
        {
            playerAudioSource.PlayOneShot(flySound);
        }
        else
        {
            if (playerAudioSource == null) Debug.LogError("playerAudioSource es nulo en PlayFlySound.");
            if (flySound == null) Debug.LogError("flySound es nulo en PlayFlySound.");
        }
    }
}