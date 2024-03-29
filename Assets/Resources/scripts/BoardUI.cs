﻿using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Binds all the UI interaction with the GameBoard.cs
/// </summary>
public class BoardUI : MonoBehaviour {

  public GameBoard board;

  public Text levelNumber;
  public Text numberOfMoves;

  // Rewind related
  public Image[] rewindHeads = new Image[3]; 
  public uint numRewindsLeft = 3;

  // Use this for initialization
  void Start() {
    levelNumber.text = "#" + (LevelManager.instance.currentLevel.levelIndex + 1);
  }

  void Update() {
    numberOfMoves.text = "" + board.currentLevel.numberOfMoves;
  }

  public void UpdateRewindHeads() {
    for (uint i = 0; i < numRewindsLeft; i++) {
      rewindHeads[i].gameObject.SetActive(true);
    }
    for (uint i = numRewindsLeft; i < 3; i++) {
      rewindHeads[i].gameObject.SetActive(false);
    }
  }

  public void PressedRewindButton() {
    if (numRewindsLeft == 0) { return; }
    numRewindsLeft--;
    UpdateRewindHeads();
    board.pressedRewindButton();
  }
}