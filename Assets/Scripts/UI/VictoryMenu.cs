using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    [SerializeField] private GameObject _victoryPanel;

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }

    public void BackToMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Time.timeScale = 1.0f;
    }

    public void Victory() {
        StartCoroutine(VictoryRoutine());
    }

    private IEnumerator VictoryRoutine() {
        yield return new WaitForSeconds(.8f); // Player Death VFX
        float delaySeconds = .5f;

        for (float t = 0f; t < delaySeconds; t += Time.deltaTime) {
            Time.timeScale = Mathf.Lerp(1f, .05f, t / delaySeconds);
            yield return null;
        }

        Time.timeScale = 0f;
        _victoryPanel.SetActive(true);
    }
}
