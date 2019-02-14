using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    /*
     *  VARIABLES
     */
    public int day = 1;
    public int hungerMeter = 10;
    public int alertLevel;
    public double money;

    // UI Elements
    public GameObject dailyChoicePanel;
    public GameObject cityPanel;
    public GameObject woodsPanel;
    public GameObject shopPanel;
    public TextMeshProUGUI dayNumber;
    public TextMeshProUGUI moneyNumber;
    public TextMeshProUGUI hungerNumber;
    public Slider hungerSlider;
    public GameObject gameOverPanel;

    // Stats (NOT IMPLEMENTED)
    public double strengthStat = 1f;
    public double agilityStat = 1f;
    public double intelligenceStat = 1f;

    // Minigames
    public GameObject jobMinigame;
    public GameObject trainingMinigame;

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
        hungerMeter += hungerChange;
        hungerSlider.value = hungerMeter;
        hungerNumber.text = hungerMeter.ToString();
        if(hungerMeter <= 0)
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
        if(success)
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
        // Check if player has money
        if (money <= 0)
        {
            return;
        }
        // Go to shop menu (Items Not Implemented)
        cityPanel.SetActive(false);
        shopPanel.SetActive(true);
    }

    // Activate Hunting Animals Minigame
    public void HuntAnimals()
    {
        woodsPanel.SetActive(false);
        EndOfDay(-1); // Move this to a finish function when minigame implemented
    }

    // Activate Train Skills Minigame
    public void TrainSkills()
    {
        woodsPanel.SetActive(false);
        trainingMinigame.SetActive(true);
    }

    // Exit Training Minigame and return to Daily Choice Menu
    public void FinishTraining()
    {
        trainingMinigame.SetActive(false);
        EndOfDay(-2);
    }
}
