using UnityEngine;

public class TurningRandomSelection : ITurningSelection
{
    public void SetStartingDirection(int startingDir)
    {
        // No need to set starting direction
    }

    public int GetNewTurningDirection()
    {
        return (Random.Range(0, 2) * 2) - 1;
    }
}

