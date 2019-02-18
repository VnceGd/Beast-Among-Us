using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : MonoBehaviour
{
    private HuntingMinigame huntingMinigame;

    // Start is called before the first frame update
    void Start()
    {
        huntingMinigame = GameObject.Find("Hunting Minigame").GetComponent<HuntingMinigame>();
    }

    // Eaten by player on collision
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            huntingMinigame.EatAnimal();
            gameObject.SetActive(false);
        }
    }
}
