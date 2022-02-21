using System;
using System.Collections.Generic;
using UnityEngine;

public class PigEvents : MonoBehaviour
{
    public static PigEvents instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }


}