using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    public void ShowDeathScreen() // called in die animation event
    {
        GameManager.Instance.ShowDeathScreen();
    }
}
