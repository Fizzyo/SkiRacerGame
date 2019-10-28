using Fizzyo;
using UnityEngine;

public static class SessionData
{
    private static FizzyoSession session;

    public static FizzyoSession Session
    {
        get
        {
            if (session == null)
            {
                session = new FizzyoSession();
            }
            return session;
        }
    }


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

    private static int score;
    public static int Score
    {
        get => score;
        set { score = value; }
    }

    private static int counter;
    public static int Counter
    {
        get => counter;
        set { counter = value; }
    }

}
