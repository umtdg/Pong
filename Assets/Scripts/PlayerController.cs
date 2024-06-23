using UnityEngine;

public class PlayerController : MonoBehaviour {
  [SerializeField] private float verticalSpeed;
  [SerializeField] private float horizontalSpeed;
  [SerializeField] private PlayerControls controls;

  private Rigidbody2D _rigidbody;

  private void Awake() {
    _rigidbody = GetComponent<Rigidbody2D>();
  }

  private void FixedUpdate() {
    var movement = ReadInput() * Time.fixedDeltaTime;

    _rigidbody.MovePosition(transform.position + movement);
  }

  private Vector3 ReadInput() {
    var verticalInput = 0.0f;
    var horizontalInput = 0.0f;

    if (Input.GetKey(controls.up)) {
      verticalInput += 1.0f;
    }

    if (Input.GetKey(controls.down)) {
      verticalInput -= 1.0f;
    }

    if (Input.GetKey(controls.left)) {
      horizontalInput -= 1.0f;
    }

    if (Input.GetKey(controls.right)) {
      horizontalInput += 1.0f;
    }

    var input = new Vector3(
      horizontalInput * horizontalSpeed,
      verticalInput * verticalSpeed,
      0.0f
    );

    return input;
  }
}
