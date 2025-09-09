using UnityEngine;

public class GoldStar : Star
{
    public int goldPoints = 20;

    protected override void GivePoints(PlayerController playerScore)
    {
        playerScore.AddPoints(goldPoints);
    }
}
