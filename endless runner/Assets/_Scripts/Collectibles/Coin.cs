using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectible
{
    public Coin()
    {
        CollectibleType = CollectibleTypes.Coin;

    }
    public override void Collect()
    {
        if(GameManager.Instance.DoublePointsOn)
        {
            GameManager.Instance.Score += 2;
        }
        else
        {
            GameManager.Instance.Score++;
        }
        base.Collect();
    }
}
