using UnityEngine;

public class TurningFlipFlopSelection : ITurningSelection
{
    private int currentDirection = 0;

    public void SetStartingDirection(int startingDir)
    {
        currentDirection = startingDir;
    }

    public int GetNewTurningDirection()
    {
        if (currentDirection == 0)
        {
            Debug.Log("A non-zero starting direction should be set before calling GetNewTurningDirection");
        }
        return currentDirection * -1;
    }
}
