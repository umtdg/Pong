using TMPro;
using UnityEngine;

public class PlayerScoreHUD : MonoBehaviour {
  [SerializeField] private PlayerScore score;

  private TextMeshProUGUI _text;

  private void Awake() {
    _text = GetComponent<TextMeshProUGUI>();
    _text.text = "0";
  }

  private void Update() { _text.text = score.score.ToString(); }
}
