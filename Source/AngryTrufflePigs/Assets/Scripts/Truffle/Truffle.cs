using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truffle : MonoBehaviour
{
    public bool WasEaten = false;
    public void Eat()
    {
        WasEaten = true;
        Destroy(gameObject);
    }

 
}
