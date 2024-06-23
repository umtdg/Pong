using UnityEngine;
using UnityEngine.Serialization;

public class ScorePost : MonoBehaviour {
    [FormerlySerializedAs("player")]
    [Tooltip("Opposite player to add score to")]
    public PlayerScore score;

    [Tooltip("Reference to ball")]
    public Ball ball;

    [Tooltip("Direction of scoringPlayer. Used to move the ball to the scoring side.")]
    public Direction playerSide;

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.gameObject.CompareTag("Ball")) return;

        score.score += 1;

        ball.ResetBall();
        ball.MoveDirection(playerSide == Direction.Left ? Vector3.left : Vector3.right);
    }

    public enum Direction {
        Left,
        Right
    }
}
