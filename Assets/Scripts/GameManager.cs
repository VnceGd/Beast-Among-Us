﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    /*
     *  VARIABLES
     */
    private readonly int MAXHUNGER = 20;
    private readonly int MAXSTAT = 10;

    private int day = 1;
    public int hungerMeter = 10;
    public int alertLevel;
    public double money;

    // UI Elements
    public GameObject statusBar;
    public GameObject dailyChoicePanel;
    public GameObject cityPanel;
    public GameObject woodsPanel;
    public GameObject shopPanel;
    public GameObject shopObjects;
    public GameObject trainingPanel;
    public TextMeshProUGUI dayNumber;
    public TextMeshProUGUI moneyNumber;
    public TextMeshProUGUI hungerNumber;
    public Slider hungerSlider;
    public GameObject gameOverPanel;

    private GameObject menuCamera;

    // Stats
    public TextMeshProUGUI speedStatText;
    public TextMeshProUGUI charismaStatText;
    public TextMeshProUGUI awarenessStatText;

    public int speedStat = 1;
    public int charismaStat = 1;
    public int awarenessStat = 1;

    // Inventory
    public GameObject inventoryPanel;
    public int forkAndKnifeCount;
    public int shieldCount;
    public int decoyCount;
    public int bearTrapCount;
    public TextMeshProUGUI forkAndKnifeCountText;
    public TextMeshProUGUI shieldCountText;
    public TextMeshProUGUI decoyCountText;
    public TextMeshProUGUI bearTrapCountText;

    // Minigames
    public GameObject jobMinigame;
    public GameObject charismaTrainingMinigame;
    public GameObject agilityTrainingMinigame;
    public GameObject awarenessTrainingMinigame;
    public GameObject huntingMinigame;

    /*
     *  FUNCTIONS
     */
    // Start is called before the first frame update
    public void Start()
    {
        menuCamera = GameObject.Find("Menu Camera");
    }

    // Load Main Menu Scene
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Show Game Over Panel
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    // Update day, hunger
    public void EndOfDay(int hungerChange)
    {
        day++;
        dayNumber.text = day.ToString();
        if (hungerChange > 0 && hungerMeter < MAXHUNGER || hungerChange < 0)
        {
            hungerMeter += hungerChange;
            hungerSlider.value = hungerMeter;
            hungerNumber.text = hungerMeter.ToString();
        }
        if (hungerMeter <= 0)
        {
            GameOver();
        }
        dailyChoicePanel.SetActive(true);
        statusBar.SetActive(true);
    }

    // Activate Work a Job Minigame
    public void WorkAJob()
    {
        statusBar.SetActive(false);
        cityPanel.SetActive(false);
        jobMinigame.SetActive(true);
        if (jobMinigame)
        {
            jobMinigame.GetComponent<JobMinigame>().UpdateLogSpeed();
        }
    }

    // Exit the Job Minigame and return to Daily Choice Menu
    public void FinishJob(bool success)
    {
        if (success)
        {
            money += charismaStat;
            moneyNumber.text = money.ToString();
        }
        jobMinigame.SetActive(false);
        EndOfDay(-2);
    }

    // Open Shop Menu
    public void BuyItem()
    {
        // Go to shop menu (Items Not Implemented)
        cityPanel.SetActive(false);
        shopPanel.SetActive(true);
        shopObjects.SetActive(true);
    }

    // Purchase an item from the shop
    public void MakePurchase(int item)
    {
        // Check if player has money
        if (money <= 0)
        {
            return;
        }
        switch (item)
        {
            case 0: // Fork and Knife
                if (money >= 3)
                {
                    forkAndKnifeCount++;
                    forkAndKnifeCountText.text = "Fork and Knife (" + forkAndKnifeCount + ")";
                    money -= 3;
                }
                break;
            case 1: // Shield
                if (money >= 2)
                {
                    shieldCount++;
                    shieldCountText.text = "Shield (" + shieldCount + ")";
                    money -= 2;
                }
                break;
            case 2: // Decoy
                if (money >= 1)
                {
                    decoyCount++;
                    decoyCountText.text = "Decoy (" + decoyCount + ")";
                    money -= 1;
                }
                break;
            case 3: // Bear Trap
                if (money >= 2)
                {
                    bearTrapCount++;
                    bearTrapCountText.text = "Bear Trap (" + bearTrapCount + ")";
                    money -= 2;
                }
                break;
        }
        moneyNumber.text = money.ToString();
        shopPanel.SetActive(false);
        shopObjects.SetActive(false);
        EndOfDay(-1);
    }

    // Activate Hunting Animals Minigame
    public void GoHunting()
    {
        statusBar.SetActive(false);
        woodsPanel.SetActive(false);
        huntingMinigame.SetActive(true);
        if (huntingMinigame.activeSelf == true)
        {
            huntingMinigame.GetComponent<HuntingMinigame>().StartMinigame();
        }
        menuCamera.SetActive(false);
    }

    // Exit Hunting Minigame and return to Daily Choice Menu
    public void FinishHunting(int hungerChange)
    {
        huntingMinigame.SetActive(false);
        menuCamera.SetActive(true);
        EndOfDay(hungerChange);
    }

    // Open Train Skills Menu
    public void TrainSkills()
    {
        woodsPanel.SetActive(false);
        trainingPanel.SetActive(true);
    }

    // Activate Train Charisma Minigame
    public void TrainCharisma()
    {
        statusBar.SetActive(false);
        trainingPanel.SetActive(false);
        charismaTrainingMinigame.SetActive(true);
    }

    // Activate Train Agility Minigame
    public void TrainAgility()
    {
        statusBar.SetActive(false);
        trainingPanel.SetActive(false);
        agilityTrainingMinigame.SetActive(true);
        menuCamera.SetActive(false);
    }

    // Activate Train Awareness Minigame
    public void TrainAwareness()
    {
        statusBar.SetActive(false);
        trainingPanel.SetActive(false);
        awarenessTrainingMinigame.SetActive(true);
        if (awarenessTrainingMinigame)
        {
            awarenessTrainingMinigame.GetComponent<AwarenessTrainingMinigame>().UpdateSwapCount();
        }
    }

    // Exit Training Minigame and return to Daily Choice Menu
    public void FinishTraining(bool success, int which)
    {
        switch (which)
        {
            case 0:
                agilityTrainingMinigame.SetActive(false);
                if (success)
                {
                    if (speedStat < MAXSTAT)
                    {
                        speedStat++;
                    }
                }
                speedStatText.text = "Speed " + speedStat;
                menuCamera.SetActive(true);
                break;
            case 1:
                charismaTrainingMinigame.SetActive(false);
                if (success)
                {
                    if (charismaStat < MAXSTAT)
                    {
                        charismaStat++;
                    }
                }
                charismaStatText.text = "Charisma " + charismaStat;
                break;
            case 2:
                awarenessTrainingMinigame.SetActive(false);
                if (success)
                {
                    if (awarenessStat < MAXSTAT)
                    {
                        awarenessStat++;
                    }
                }
                awarenessStatText.text = "Awareness " + awarenessStat;
                break;
        }
        EndOfDay(-2);
    }
}
