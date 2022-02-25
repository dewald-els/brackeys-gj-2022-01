using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truffle : MonoBehaviour
{
    [SerializeField] AudioSource sound;
    public bool WasEaten = false;
    public void Eat()
    {
        WasEaten = true;
        Destroy(gameObject);
    }

    public void Place()
    {
        sound.PlayOneShot(sound.clip);
    }

}
