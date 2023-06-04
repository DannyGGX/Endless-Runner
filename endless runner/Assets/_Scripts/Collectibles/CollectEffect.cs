using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectEffect : MonoBehaviour
{
    private void Awake()
    {
        Invoke(nameof(Destroy), 1.5f);
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
