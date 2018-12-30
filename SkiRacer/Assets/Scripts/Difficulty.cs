using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Difficulty {

	private static float secondsToMax = 3;
    private static int diff = 1;

	public static float GetDifficultyPercent(int counter)
    {
        return Mathf.Clamp01(diff / secondsToMax);
	}

    public static int Diff
    {
        get { return diff; }
        set { diff = value; }
    }
}
