﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterLevelMenuManager : MonoBehaviour {

	public GameObject nextLevelButton;
  public List<Image> rewindHeads;
  public Text coinsRewardedThisLevel;
  public Text coinsTotal;
  public Text info;

  void Start() {
    info.text = "Moves: " + LevelManager.instance.currentLevel.numberOfMoves;
    coinsRewardedThisLevel.text = "";
    uint coinsRewarded = (uint)LevelManager.instance.currentLevel.numRewindsLeft + LevelManager.instance.currentLevel.numCoinsRewarded;
    coinsTotal.text = "" + (PlayerPrefs.GetInt("coins") - coinsRewarded);

    if (LevelManager.instance.currentLevel.completed) { return; }

    if (LevelManager.instance.nextLevel() == null) { nextLevelButton.SetActive(false); }

    if (!LevelManager.instance.nextLevel().unlocked) {
      if (LevelManager.instance.nextLevel().unlockPrice > PlayerPrefs.GetInt("coins")) {
        nextLevelButton.GetComponent<Text>().text = "Watch ad";
      } else {
        nextLevelButton.GetComponent<Text>().text = "Unlock level";
      }
    }

    StartCoroutine(RewindHeadCoinAnimation());
  }

  public void nextLevelButtonPressed() {
    int levelIndex = LevelManager.instance.currentLevel.levelIndex + 1;
    if (levelIndex >= LevelManager.instance.levels.Length) { return; }

    if (!LevelManager.instance.nextLevel().unlocked) {
      if (LevelManager.instance.nextLevel().unlockPrice > PlayerPrefs.GetInt("coins")) {
        // TODO: Watch ad
      } else {
        // TODO: Spend gold, unlock next level
      }
    }

		LevelManager.instance.loadNextLevel();
		StartCoroutine(GetComponent<Fading>().LoadScene("scenes/playing"));
	}

	public void LevelSelectionButtonPressed() {
    StartCoroutine(GetComponent<Fading>().LoadScene("scenes/levelselectionmenu"));
  }

  public IEnumerator RewindHeadCoinAnimation() {
    const float dt = 0.2f; // 0.2f gives retro feel

    for (int j = 0; j < (int) (1.0f / dt) + 2; j++) {
      for (int i = 0; i < LevelManager.instance.currentLevel.numRewindsLeft; i++) {
        rewindHeads[i].transform.localScale = new Vector3(j * dt, j * dt, 1.0f);
      }
      yield return new WaitForSeconds(dt);
    }

    uint coinsRewarded = (uint)LevelManager.instance.currentLevel.numRewindsLeft + LevelManager.instance.currentLevel.numCoinsRewarded;
    for (int i = 0; i < coinsRewarded; i++) {
      coinsRewardedThisLevel.text = "+" + (i + 1);
      coinsTotal.text = "" + (PlayerPrefs.GetInt("coins") - coinsRewarded + i + 1);
      yield return new WaitForSeconds(dt);
    }
  }
}
