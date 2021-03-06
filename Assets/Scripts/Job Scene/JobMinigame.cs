﻿using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JobMinigame : MonoBehaviour
{
    public GameObject manager;
    private GameManager gameManager;

    public GameObject countdown;
    private TextMeshProUGUI countdownTimerText;
    public float countdownTimer = 2f;
    public bool countdownActive = true;

    public GameObject woodLog;
    private LogMotion logMotion;

    public GameObject successArray;
    private Toggle[] successToggles;

    public int logCount = 5;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Game Manager");
        gameManager = manager.GetComponent<GameManager>();

        countdownTimerText = countdown.GetComponent<TextMeshProUGUI>();

        logMotion = woodLog.GetComponent<LogMotion>();

        successToggles = successArray.GetComponentsInChildren<Toggle>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (countdownActive)
        {
            countdownTimer -= Time.deltaTime;
            countdownTimerText.text = countdownTimer.ToString("0.0");
            if (countdownTimer <= 0f)
            {
                countdownActive = false;
                StartMinigame();
            }
        }
    }

    // Show wood log and start minigame
    public void StartMinigame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        woodLog.SetActive(true);
        countdown.SetActive(false);
        UpdateLogSpeed();
    }

    // Reset varialbes to starting values
    public void ResetMinigame()
    {
        logMotion.ResetLog();
        woodLog.SetActive(false);

        foreach (Toggle tog in successToggles)
        {
            tog.isOn = false;
        }

        logCount = 3;
        countdownTimer = 2f;
        countdownActive = true;
        countdown.SetActive(true);

        gameObject.SetActive(false);
    }

    // Call FinishJob() from Game Manager
    public void EndMinigame(bool success)
    {
        Cursor.lockState = CursorLockMode.None;
        ResetMinigame();
        gameManager.FinishJob(success);
    }

    // Update UI to show successful attempts
    public void ChopLog(bool sweetSpotHit)
    {
        // Visual indicator of successful wood chopping
        if (sweetSpotHit)
        {
            foreach (Toggle tog in successToggles)
            {
                if (!tog.isOn)
                {
                    tog.isOn = true;
                    break;
                }
            }
        }
        logCount--;
        logMotion.logSpeed *= 1.25f;
        if (logCount <= 0)
        {
            foreach (Toggle tog in successToggles)
            {
                if (!tog.isOn)
                {
                    EndMinigame(false);
                    return;
                }
            }
            EndMinigame(true);
            return;
        }
    }

    // Incrase log speed based on current Charisma stat
    public void UpdateLogSpeed()
    {
        if (gameManager)
        {
            logMotion.logSpeed = -2f - (gameManager.charismaStat * 0.25f);
        }
    }
}
