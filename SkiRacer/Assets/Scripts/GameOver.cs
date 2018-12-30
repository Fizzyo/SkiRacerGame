using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fizzyo;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverScreen;
    public Text pointsScoredUI;
    public PlayerControl player;

    private bool gameOver;

    void Start()
    {
        player.OnGameOver += OnGameOver;
        gameOver = false;
    }

    void Update()
    {
        if (gameOver)
        {
            FizzyoFramework.Instance.Achievements.PostScore(int.Parse(player.pointsTxt.text));
        }
    }

    void OnGameOver()
    {
        gameOverScreen.SetActive(true);
        pointsScoredUI.text = player.pointsTxt.text;
        gameOver = true;
    }

	public void Restart()
    {
        gameOver = false;
		SceneManager.LoadScene("StartScreen");
	}
}
