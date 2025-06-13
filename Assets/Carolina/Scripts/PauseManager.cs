using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseCanvas; // Asignas aquí el Canvas de Pausa

    private bool isPaused = false;

    void Start()
    {
        if (pauseCanvas != null)
            pauseCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        if (pauseCanvas != null)
            pauseCanvas.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        if (pauseCanvas != null)
            pauseCanvas.SetActive(false);
    }

    // Este es el método que llamará el botón "Continuar"
    public void OnResumeButton()
    {
        ResumeGame();
    }
}


