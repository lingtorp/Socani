﻿using UnityEngine;

public class Player : MonoBehaviour {
	// Private
	public const float MOVECOOLDOWN = 0.25f;
  public float timeSinceLastMove = 0.0f;

	// Player board tile representation
	public Tile tile;
  public GameBoard board;
  private Rigidbody2D rb2D;
  public Animator animator;

	void Start() {
    animator = GetComponent<Animator>();
    board = FindObjectOfType<GameBoard>();
		tile = GetComponent<Tile>();
    // Disable rotation on the RB
    rb2D = GetComponent<Rigidbody2D>();
    rb2D.freezeRotation = true;
    // Setup internal tile in the GameBoard
    tile.boardPosition = board.world_to_board_position(transform.position);
    Vector2Int position = new Vector2Int(tile.boardPosition.x, tile.boardPosition.y);
    board.board[position].Add(gameObject);
	}
 
	void Update() {
		timeSinceLastMove += Time.deltaTime;
    if (timeSinceLastMove <= MOVECOOLDOWN) { return; }
		bool did_move = false;
		if (Input.GetKey("up")) {
      animator.Play("Walking Up");
      did_move = board.validMove(gameObject, tile.boardPosition, Vector3Int.up);
		}
    else if (Input.GetKey("down")) {
      animator.Play("Walking Down");
      did_move = board.validMove(gameObject, tile.boardPosition, Vector3Int.down);
    }
    else if (Input.GetKey("left")) {
      animator.Play("Walking Left");
      did_move = board.validMove(gameObject, tile.boardPosition, Vector3Int.left);
    }
    else if (Input.GetKey("right")) {
      animator.Play("Walking Right");
      did_move = board.validMove(gameObject, tile.boardPosition, Vector3Int.right);
    }
    if (did_move) {
      AudioManager.instance.Play("step");
			board.endMove();
			timeSinceLastMove = 0.0f;
      if (board.CheckGamestate()) {
        AudioManager.instance.Play("bonus");
      }
    }
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    Debug.Log(string.Format("Player hit {0}.", collision.gameObject.name));
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    Debug.Log(string.Format("Player hit {0}.", collision.gameObject.name));
  }
}