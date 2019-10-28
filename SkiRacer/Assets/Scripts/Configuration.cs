using Fizzyo;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Configuration : MonoBehaviour {

    public Text Laps;
    public Text Breaths;

    private int laps = 3;
    private int counter = 8;
    private int diff = 0;

	public void PlayGame()
    {
        SessionData.Session.SessionSetCount = int.Parse(Laps.text);
        SessionData.Session.SessionBreathCount = int.Parse(Breaths.text);
        SessionData.Diff = diff;
        SceneManager.LoadScene(1);
	}

	public void QuitGame()
    {
		Application.Quit();
	}

	public int GetBreaths()
    {
		return counter;
	}
    public void DecreaseLaps()
    {
        if (laps > 1)
            laps--;

        Laps.text = laps.ToString();
    }

    public void IncreaseLaps()
    {
        if (laps < 100)
            laps++;

        Laps.text = laps.ToString();
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