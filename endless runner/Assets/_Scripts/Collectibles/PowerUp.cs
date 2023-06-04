using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Collectible
{
    public enum PowerUpTypes
    {
        DoublePoints,
        SlowDown
    }
    public PowerUpTypes PowerUpType;

    public PowerUp()
    {
        CollectibleType = CollectibleTypes.PowerUp;
    }
}
