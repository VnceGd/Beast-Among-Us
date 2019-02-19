using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HuntingMinigame : MonoBehaviour
{
    private readonly float STARTTIME = 20f;

    private GameObject manager;
    private GameManager gameManager;

    private SpawnHunters hunterSpawner;
    private SpawnAnimals animalSpawner;

    private GameObject player;
    private PlayerController playerController;
    private Transform playerTransform;

    public TextMeshProUGUI timerText;
    public float timer;
    public Slider quotaProgress;
    public int quota = 10;
    public int quantityEaten;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Game Manager");
        gameManager = manager.GetComponent<GameManager>();

        hunterSpawner = GameObject.Find("Hunter Spawner").GetComponent<SpawnHunters>();
        animalSpawner = GameObject.Find("Animal Spawner").GetComponent<SpawnAnimals>();

        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerTransform = player.transform;

        StartMinigame();
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

    public void StartMinigame()
    {
        quotaProgress.maxValue = quota * 1.5f;
        timer = STARTTIME;
        animalSpawner.Spawn();
        hunterSpawner.Spawn();
    }

    // Reset variables to starting values
    public void ResetMinigame()
    {
        timer = STARTTIME;
        timerText.text = "0.0";
        quantityEaten = 0;
        quotaProgress.value = 0;
        playerTransform.position = Vector3.up;
        playerController.UpdateSpeedStat();
        animalSpawner.Despawn();
        animalSpawner.Spawn();
        if (animalSpawner.animalCount > 5)
        {
            animalSpawner.animalCount--;
        }
        hunterSpawner.hunterCount = gameManager.alertLevel;
        hunterSpawner.Despawn();
        hunterSpawner.Spawn();
    }

    // Reward hunger based on how much of quota was fulfilled
    public void EndMinigame()
    {
        int hungerChange = 0;
        if (quantityEaten < (quota / 2))
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
        gameManager.alertLevel++;
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
