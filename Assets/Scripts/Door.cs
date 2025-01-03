using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractuable
{
    public void Interactuar()
    {
        Debug.Log("Me abro!");
    }
}
