using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePoints : PowerUp
{
    [SerializeField] private float doublePointsDuration = 15;
    public DoublePoints()
    {
        PowerUpType = PowerUpTypes.DoublePoints;
    }

    public override void Collect()
    {
        // Double points for 15 seconds
        GameManager.Instance.DoublePointsOn = true;
        base.Collect();
        Invoke(nameof(Deactivate), doublePointsDuration);
    }

    public void Deactivate()
    {
        GameManager.Instance.DoublePointsOn = false;

    }
}
