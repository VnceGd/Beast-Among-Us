using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HuntingMinigame : MonoBehaviour
{
    private GameObject manager;
    private GameManager gameManager;

    private SpawnAnimals animalSpawner;
    private Transform playerTransform;

    public TextMeshProUGUI timerText;
    public float timer = 10f;
    public Slider quotaProgress;
    public int quota = 10;
    public int quantityEaten;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Game Manager");
        gameManager = manager.GetComponent<GameManager>();

        animalSpawner = GameObject.Find("Animal Spawner").GetComponent<SpawnAnimals>();
        playerTransform = GameObject.FindWithTag("Player").transform;

        ResetMinigame();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = timer.ToString("0.0");
        if (timer < 0f)
        {
            EndMinigame();
        }
    }

    // Reset variables to starting values
    public void ResetMinigame()
    {
        timer = 10f;
        timerText.text = "0.0";
        quantityEaten = 0;
        quotaProgress.value = 0;
        playerTransform.position = Vector3.up;
        animalSpawner.Despawn();
        animalSpawner.Spawn();
    }

    // Reward hunger based on how much of quota was fulfilled
    public void EndMinigame()
    {
        int hungerChange = 0;
        if (quantityEaten == 0)
        {
            hungerChange = -2;
        }
        else if (quantityEaten < (quota / 2))
        {
            hungerChange = -1;
        }
        else if (quantityEaten < quota)
        {
            hungerChange = 0;
        }
        else if (quantityEaten < (quota * 1.5))
        {
            hungerChange = 1;
        }
        else
        {
            hungerChange = 2;
        }
        ResetMinigame();
        gameManager.FinishHunting(hungerChange);
    }

    // Increase quantity eaten after collision with animal
    public void EatAnimal()
    {
        quantityEaten++;
        quotaProgress.value++;
    }
}
