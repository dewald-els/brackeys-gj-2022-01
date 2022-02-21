using System;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public static PlayerEvents instance;

    private void Awake()
    {
        instance = this;
    }

    public event Action<Truffle> OnPlayerPlacedTruffle;
    public void PlayerPlacedTruffle(Truffle truffle)
    {
        if (OnPlayerPlacedTruffle != null && truffle != null)
        {
            OnPlayerPlacedTruffle(truffle);
        }
    }
}
