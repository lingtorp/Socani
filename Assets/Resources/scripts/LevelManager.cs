﻿using UnityEngine;
using System;

[System.Serializable]
public class LevelManager : MonoBehaviour {
  public static LevelManager instance;

  // All the levels in the game
  public Level[] levels;

  // The lvl that is going be to played by the GameBoard
  public Level currentLevel; // Level serialized and saved 

  // Enables everything in the game for testing purposes
  public bool godMode = false;

  // If enabled on startup, resets all progress
  public bool reset = false;

  void Awake() {
    if (instance == null) {
      instance = this;
    } else {
      Destroy(gameObject);
      return;
    }

    DontDestroyOnLoad(this);

    if (reset) {
      for (int i = 0; i < 3; i++) {
        levels[i].Reset();
        levels[i].unlocked = true;
      }
      for (int i = 3; i < levels.Length; i++) {
        levels[i].Reset();
      }
      PlayerPrefs.SetInt("coins", 0);
    }

    if (godMode) {
      PlayerPrefs.SetInt("coins", 50);
    }
    PlayerPrefs.Save();
  }

  public Level getLevel() {
		if (currentLevel) {
			return Instantiate(currentLevel);
		} else {
			Debug.LogWarning("Scene 'player' started without a current level");
			return Instantiate(levels[0]);
		}
  }

  public bool loadNextLevel() {
    // Out of levels! End of game?
    Debug.Log("Loading level:" + (currentLevel.levelIndex + 1));
    if (levels.Length <= currentLevel.levelIndex + 1) {
			return false;
		}
		currentLevel = levels[currentLevel.levelIndex + 1];
		return true;
  }

	public Level nextLevel() {
    if (levels.Length <= currentLevel.levelIndex + 1) {
			return null;
		}
		return levels[currentLevel.levelIndex + 1];
	}

	// Returns whether or not it found the level 
	public bool levelCompleted(Level level) {
		Level levelPrefab = Array.Find(levels, match => match.levelIndex == level.levelIndex);
    if (!levelPrefab.completed) {
      int coinsAwarded = PlayerPrefs.GetInt("coins") + (int) level.numRewindsLeft + (int) level.numCoinsRewarded;
      PlayerPrefs.SetInt("coins", coinsAwarded);
      PlayerPrefs.Save();
    }
		if (levelPrefab) {
      levelPrefab.completed = true;
      levelPrefab.numberOfMoves = level.numberOfMoves;
      levelPrefab.numRewindsLeft = level.numRewindsLeft;
      levelPrefab.numCoinsRewarded = level.numCoinsRewarded;
      return true;
		}
		return false;
	}
}