using UnityEngine;

public class ComputerController : MonoBehaviour {
  [SerializeField] private Ball ball;
  [SerializeField] private float speed;

  [Tooltip("Reaction speed in terms of FixedUpdate ticks")]
  [SerializeField]
  private uint reactionSpeed;

  private Rigidbody2D _rigidbody;
  private float _ballY;
  private uint _reactionTimer;
  private bool _reacted;

  private void Awake() { _rigidbody = GetComponent<Rigidbody2D>(); }

  private void FixedUpdate() { TrackBall(); }

  private void TrackBall() {
    if (!ball.Trackable) {
      MoveToPosition(Vector3.zero);
      _reacted = false;
      _reactionTimer = 0;
      return;
    }

    if (!_reacted) {
      _reactionTimer++;
      if (_reactionTimer > reactionSpeed) {
        _reacted = true;
      }

      if (!_reacted) return;
    }

    _ballY = ball.transform.position.y;
    MoveToPosition(new Vector3(0.0f, _ballY, 0.0f));
  }

  private void MoveToPosition(Vector3 pos) {
    var currentY = transform.position.y;
    var input = Vector3.zero;

    if (pos.y > currentY)
      input.y = 1.0f;
    else if (pos.y < currentY) input.y = -1.0f;

    input.y *= speed * Time.fixedDeltaTime;

    var target = transform.position + input;
    target.y = input.y switch {
      > 0.0f => Mathf.Min(pos.y, target.y),
      < 0.0f => Mathf.Max(pos.y, target.y),
      _ => target.y
    };

    _rigidbody.MovePosition(target);
  }
}
