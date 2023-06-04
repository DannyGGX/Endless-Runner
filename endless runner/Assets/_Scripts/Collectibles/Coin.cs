using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectible
{
    public Coin()
    {
        CollectibleType = CollectibleTypes.Coin;
    }

    [SerializeField] private int coinScoreValue = 1;

    public override void Collect()
    {
        if(GameManager.Instance.DoublePointsOn)
        {
            GameManager.Instance.Score += coinScoreValue * 2;
        }
        else
        {
            GameManager.Instance.Score += coinScoreValue;
        }
        base.Collect();
    }
}
