using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
  [SerializeField] private string gameSceneName;

  public void PlayGame() {
    Time.timeScale = 0.0f;
    SceneManager.LoadScene(gameSceneName);
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
