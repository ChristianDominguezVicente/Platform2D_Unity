using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IInteractuable
{
    public void Interactuar()
    {
        Destroy(gameObject);
    }
}
