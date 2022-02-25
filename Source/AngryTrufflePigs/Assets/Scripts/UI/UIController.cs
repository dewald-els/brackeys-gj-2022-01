using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text pigInFenceText;
    [SerializeField] private int totalPigs;
    private int pigsInFence = 0;

    void Start()
    {
        PigEvents.instance.OnPigInFence += PigInFence;
    }

    private void PigInFence()
    {
        pigsInFence++;
        pigInFenceText.text = "Pigs: " + pigsInFence.ToString();
    }

    private void OnDestroy()
    {
        if (PigEvents.instance != null)
        {
            PigEvents.instance.OnPigInFence -= PigInFence;
        }
    }
}
