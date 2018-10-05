using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public GameObject gameOverScreen;
	public Text pointsScoredUI;
	bool gameOver;

	void Start(){
		FindObjectOfType<PlayerControl> ().OnGameOver += OnGameOver;

	}

	void Update () {
		if (gameOver) {
			if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene (0);
			}
		}
	}

	void OnGameOver(){
		gameOverScreen.SetActive (true);
		pointsScoredUI.text = player.Points.text; 
		gameOver = true;
	}
}
