using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : Collectible
{
    public enum PowerUpTypes
    {
        DoublePoints,
    }
    public PowerUpTypes PowerUpType;
    public PowerUp()
    {

        CollectibleType = CollectibleTypes.PowerUp;
    }

    void Update()
    {
        
    }

    
}
