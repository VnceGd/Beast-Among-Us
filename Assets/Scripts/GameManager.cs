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

    private int day = 1;
    public int hungerMeter = 10;
    public int alertLevel;
    public double money;

    // UI Elements
    public GameObject dailyChoicePanel;
    public GameObject cityPanel;
    public GameObject woodsPanel;
    public GameObject shopPanel;
    public GameObject trainingPanel;
    public TextMeshProUGUI dayNumber;
    public TextMeshProUGUI moneyNumber;
    public TextMeshProUGUI hungerNumber;
    public Slider hungerSlider;
    public GameObject gameOverPanel;

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

    /*
     *  FUNCTIONS
     */
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
    }

    // Activate Killing People Minigame
    public void KillPeople()
    {
        cityPanel.SetActive(false);
        alertLevel++;
        EndOfDay(1); // Move this to a finish function when minigame implemented
    }

    // Activate Work a Job Minigame
    public void WorkAJob()
    {
        cityPanel.SetActive(false);
        jobMinigame.SetActive(true);
    }

    // Exit the Job Minigame and return to Daily Choice Menu
    public void FinishJob(bool success)
    {
        if (success)
        {
            money++;
            moneyNumber.text = money.ToString("0");
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
    }

    // Purchase an item from the shop
    public void MakePurchase(int item)
    {
        // Check if player has money
        if (money <= 0)
        {
            return;
        }
        switch(item)
        {
            case 0: // Fork and Knife
                if(money >= 3)
                {
                    forkAndKnifeCount++;
                    forkAndKnifeCountText.text = "Fork and Knife (" + forkAndKnifeCount + ")";
                    money -= 3;
                }
                break;
            case 1: // Shield
                if(money >= 2)
                {
                    shieldCount++;
                    shieldCountText.text = "Shield (" + shieldCount + ")";
                    money -= 2;
                }
                break;
            case 2: // Decoy
                if(money >= 1)
                {
                    decoyCount++;
                    decoyCountText.text = "Decoy (" + decoyCount + ")";
                    money -= 1;
                }
                break;
            case 3: // Bear Trap
                if(money >= 2)
                {
                    bearTrapCount++;
                    bearTrapCountText.text = "Bear Trap (" + bearTrapCount + ")";
                    money -= 2;
                }
                break;
        }
        moneyNumber.text = money.ToString();
        shopPanel.SetActive(false);
        EndOfDay(-2);
    }

    // Activate Hunting Animals Minigame
    public void HuntAnimals()
    {
        woodsPanel.SetActive(false);
        EndOfDay(-1); // Move this to a finish function when minigame implemented
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
        trainingPanel.SetActive(false);
        charismaTrainingMinigame.SetActive(true);
    }

    public void TrainAgility()
    {
        trainingPanel.SetActive(false);
        agilityTrainingMinigame.SetActive(true);
    }

    // Exit Training Minigame and return to Daily Choice Menu
    public void FinishTraining(int which)
    {
        switch(which) {
            case 0:
                agilityTrainingMinigame.SetActive(false);
                speedStatText.text = "Speed " + speedStat;
                break;
            case 1:
                charismaTrainingMinigame.SetActive(false);
                charismaStatText.text = "Charisma " + charismaStat;
                break;
            case 2:
                // Finish Awareness Training Minigame 
                break;
        }
        EndOfDay(-2);
    }
}
