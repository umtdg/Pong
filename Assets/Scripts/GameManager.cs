using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
  [SerializeField] private GameObject ballObject;
  [SerializeField] private uint countdown;
  [SerializeField] private TextMeshProUGUI countdownText;

  public GameState CurrentState { get; private set; }

  private void Awake() {
    RemoveState(GameState.Playing);
    RemoveState(GameState.Paused);
    AddState(GameState.Countdown);
  }

  private void Start() {
    countdownText.text = countdown.ToString();
    StartCoroutine(Countdown(countdown));
  }

  private IEnumerator Countdown(uint seconds) {
    while (seconds > 0) {
      countdownText.text = seconds.ToString();
      yield return new WaitForSecondsRealtime(1);
      seconds--;
    }

    RemoveState(GameState.Countdown);
    AddState(GameState.Playing);
    Time.timeScale = 1.0f;
    ballObject.SetActive(true);
    countdownText.gameObject.SetActive(false);
  }

  public bool CheckState(GameState state) { return (CurrentState & state) == state; }

  public void AddState(GameState state) { CurrentState |= state; }

  public void RemoveState(GameState state) { CurrentState &= ~state; }

  public void TogglePlayPause() {
    if (CheckState(GameState.Countdown)) return;

    if (CheckState(GameState.Playing)) {
      AddState(GameState.Paused);
      RemoveState(GameState.Playing);
    } else if (CheckState(GameState.Paused)) {
      AddState(GameState.Playing);
      RemoveState(GameState.Paused);
    }
  }

  [Flags]
  public enum GameState {
    Countdown,
    Playing,
    Paused
  }
}
