﻿using UnityEngine;

public class Coin : MonoBehaviour {
  private void OnTriggerEnter2D(Collider2D col) {
        GameBoard board = FindObjectOfType<GameBoard>();
        if (board) {
            var position = board.world_to_board_position(transform.position);
            if (board.remove_from_board(gameObject, position)) {
                Player player = col.gameObject.GetComponent<Player>();
                if (player) {
                    board.currentLevel.numCoinsRewarded++;
                    AudioManager.instance.Play("coin-pickup");
                } else {
                    AudioManager.instance.Play("coin-destroy");
                }
                // TODO: Animate, positive feeling, negative feeling
                Destroy(gameObject);
            } else {
              Debug.LogError("Crap"); // Should not happen ...  
            }
        }
    }
}
