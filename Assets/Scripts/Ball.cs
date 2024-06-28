using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour {
  [SerializeField] private Vector3 initialPosition = Vector3.zero;
  [SerializeField] private float initialSpeed;
  [SerializeField] private float speedIncrease;
  [SerializeField] private float yTolerance;
  [SerializeField] private float minXSpeed;

  private Vector3 _initialDirection;
  private Rigidbody2D _rigidbody;

  public bool Trackable { get; private set; }
  private float _speed;
  private Vector2 _velocity;

  private void Awake() {
    _rigidbody = GetComponent<Rigidbody2D>();

    Trackable = true;
    _speed = initialSpeed;
    _initialDirection = Random.Range(0, 2) == 0 ? Vector3.left : Vector3.right;

    MoveDirection(_initialDirection);
  }

  private void OnCollisionEnter2D(Collision2D other) {
    var isPlayer = other.gameObject.CompareTag("Player");
    var isComputer = other.gameObject.CompareTag("Computer");
    var isWall = other.gameObject.CompareTag("Wall");

    if (isPlayer || isComputer) CollideWithPaddle(isPlayer, other.gameObject.transform.position);
    if (isWall) CollideWithWall();
  }

  private Vector3 CalculateDirection(Vector3 paddlePos, Vector3 ballPos) {
    var direction = ballPos - paddlePos;

    if (Mathf.Abs(paddlePos.y - ballPos.y) < yTolerance) {
      direction.y = paddlePos.y;
    }

    if (direction.x < Mathf.Epsilon) direction.x = ballPos.x > 0.0f ? -0.1f : 0.1f;

    return direction.normalized;
  }

  private void CollideWithWall() {
    _velocity.y = -_velocity.y;
    _rigidbody.velocity = _velocity;
  }

  private void CollideWithPaddle(bool isPlayer, Vector3 paddlePos) {
    _speed += speedIncrease;
    Trackable = isPlayer;

    var ballPos = gameObject.transform.position;
    var direction = CalculateDirection(paddlePos, ballPos);

    MoveDirection(direction);
  }

  public void MoveDirection(Vector3 direction) {
    _velocity = _speed * direction;

    var velX = Mathf.Max(Mathf.Abs(minXSpeed), Mathf.Abs(_velocity.x));
    var sign = Mathf.Sign(_velocity.x);
    _velocity.x = sign * velX;

    _velocity = _speed * _velocity.normalized;
    _rigidbody.velocity = _velocity;
  }

  public void ResetBall() {
    _speed = initialSpeed;
    transform.position = initialPosition;
    Trackable = true;
  }
}
