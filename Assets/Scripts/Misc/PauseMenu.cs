using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour {

    [SerializeField] private GameObject _pausePanel;

    public void Pause() {
        _pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void BackToMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Time.timeScale = 1.0f;
    }

    public void Resume() {
        _pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
