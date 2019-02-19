using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgilityDeathZone : MonoBehaviour
{
    private AgilityTrainingMinigame agilityManager;

    void Start()
    {
        agilityManager = GetComponentInParent<AgilityTrainingMinigame>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.name == "Player (Minigame)")
        {
            agilityManager.EndMinigame(false);
        }
    }
}
