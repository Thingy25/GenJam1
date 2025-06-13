using UnityEngine;
using System;

public class SoundCharacterOneManager : MonoBehaviour
{
    [Header("Referencias")]
    // Referencia al controlador del personaje uno (debería estar en el mismo GameObject)
    [SerializeField] private CharacterOneControllerWilson characterOneController;
    // Referencia al AudioSource compartido en la cámara
    private AudioSource playerAudioSource;

    [Header("Clips de Sonido")]
    [SerializeField] private AudioClip jumpSound; // Sonido al saltar

    void Awake()
    {
        // Asegúrate de que el controlador esté asignado o lo busca en este mismo GameObject
        if (characterOneController == null)
        {
            characterOneController = GetComponent<CharacterOneControllerWilson>();
        }

        // --- Obtiene el AudioSource de la cámara principal ---
        playerAudioSource = Camera.main.GetComponent<AudioSource>();

        if (playerAudioSource == null)
        {
            Debug.LogError("SoundCharacterOneManager: No se encontró un AudioSource en la Cámara Principal. Asegúrate de añadir uno.");
        }
    }

    void OnEnable()
    {
        // Suscribirse al evento de salto solo si el controlador y la fuente de audio existen
        if (characterOneController != null && playerAudioSource != null)
        {
            characterOneController.OnJumpStarted += PlayJumpSound;
        }
    }

    void OnDisable()
    {
        // Desuscribirse del evento
        if (characterOneController != null && playerAudioSource != null)
        {
            characterOneController.OnJumpStarted -= PlayJumpSound;
        }
    }

    // Función para reproducir el sonido de salto
    private void PlayJumpSound()
    {
        Debug.Log("PlayJumpSound llamado, intentando reproducir."); // <-- Añade esta línea
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
}