using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTruffle : MonoBehaviour
{
    [Header("Truffles")]
    [SerializeField] private Truffle truffle;
    [SerializeField] private int maximunTruffles = 5;
    [SerializeField] private TMP_Text tmpTruffleCount;
    private int trufflesPlaced = 0;

    private void Start()
    {
        tmpTruffleCount.text = "Truffles: " + maximunTruffles.ToString();
    }

    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            PlaceTruffle();
        }
    }

    void PlaceTruffle()
    {
        if (trufflesPlaced < maximunTruffles)
        {
            trufflesPlaced++;
            tmpTruffleCount.text = "Truffles: " + (maximunTruffles - trufflesPlaced).ToString();
            Truffle _truffle = Instantiate(truffle, transform.position, transform.rotation);
            PlayerEvents.instance.PlayerPlacedTruffle(_truffle);
        }
    }
}
