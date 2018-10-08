using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Configuration : MonoBehaviour {

    public Text Breaths;

    private int counter = 1;
    private int diff = 0;

	public void PlayGame()
    {
        BreathsCount.BreathsPer = counter;
        Difficulty.Diff = diff;
        SceneManager.LoadScene(2);
	}

	public void QuitGame()
    {
		Application.Quit();
	}

	public int GetBreaths()
    {
		return counter;
	}

	public void DecreaseBreaths()
    {
		if (counter > 1)
			counter--;

		Breaths.text = counter.ToString();
	}

	public void IncreaseBreaths()
    {
		if (counter < 100)
			counter++;
			
		Breaths.text = counter.ToString();
	}

    public void DifficultyChanged(int value)
    {
        diff = value;
    }
}
