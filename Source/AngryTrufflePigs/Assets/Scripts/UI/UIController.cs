using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject completeScreen;
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

        if (pigsInFence == totalPigs)
        {
            // Level Done!
            completeScreen.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }



    private void OnDestroy()
    {
        if (PigEvents.instance != null)
        {
            PigEvents.instance.OnPigInFence -= PigInFence;
        }
    }
}
