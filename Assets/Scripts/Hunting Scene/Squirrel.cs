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
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            huntingMinigame.EatAnimal(other.GetComponent<PlayerController>().fakActive);
            Destroy(gameObject);
        }
    }
}
