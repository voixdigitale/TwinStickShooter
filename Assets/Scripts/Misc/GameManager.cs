using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public bool GameIsOver {  get; private set; }

    private PauseMenu _pauseMenu;
    private GameOverMenu _gameOverMenu;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
        }

        _pauseMenu = GetComponent<PauseMenu>();
        _gameOverMenu = GetComponent<GameOverMenu>();
    }

    private void Start() {
        GameIsOver = false;
    }

    public void PauseGame() {
        _pauseMenu.Pause();
    }

    public void GameOver() {
        GameIsOver = true;
        _gameOverMenu.GameOver();
    }
}
