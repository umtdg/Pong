using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
  [SerializeField] private GameManager gameManager;
  [SerializeField] private GameObject pauseMenuElements;
  [SerializeField] private string mainMenuScene;

  private void Update() {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      gameManager.TogglePlayPause();
      if (gameManager.CheckState(GameManager.GameState.Paused)) {
        Resume();
      } else if (gameManager.CheckState(GameManager.GameState.Playing)) {
        Pause();
      }
    }
  }

  private void OnApplicationPause(bool pauseStatus) {
    if (pauseStatus && gameManager.CheckState(GameManager.GameState.Playing)) {
      Pause();
    }
  }

  private void Pause() {
    pauseMenuElements.SetActive(true);
    Time.timeScale = 0.0f;
  }

  public void Resume() {
    pauseMenuElements.SetActive(false);
    Time.timeScale = 1.0f;
  }

  public void LoadMenu() {
    Time.timeScale = 1.0f;
    SceneManager.LoadScene(mainMenuScene);
  }

  public void ExitGame() {
#if UNITY_EDITOR
    if (Application.isEditor) {
      EditorApplication.ExitPlaymode();
    } else {
      Application.Quit();
    }
#else
    Application.Quit();
#endif
  }
}
