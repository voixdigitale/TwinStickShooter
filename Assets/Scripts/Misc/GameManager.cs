using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private PauseMenu _pauseMenu;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
        }

        _pauseMenu = GetComponent<PauseMenu>();
    }

    public void PauseGame() {
        _pauseMenu.Pause();
    }
}
