using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgilityTrainingMinigame : MonoBehaviour
{
    public GameObject[] col1;

    public GameObject[] col2;

    public GameObject[] col3;

    public GameObject[] col4;

    public GameObject[] col5;

    public GameObject player;

    public GameObject winZone;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        resetMap();
        randomMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void randomMap()
    {
        int prevBranch = 0;
        for(int i = 1; i <= 5; i++)
        {
            prevBranch = selectBranch(i, prevBranch);
        }
    }

    void resetMap()
    {
        for(int i = 0; i < 5; i++)
        {
            col1[i].SetActive(false);
            col2[i].SetActive(false);
            col3[i].SetActive(false);
            col4[i].SetActive(false);
            col5[i].SetActive(false);
        }
        player.transform.position = Vector3.zero;
    }

    int selectBranch(int colNum, int branch)
    {
        switch(colNum)
        {
            case 1:
            {
                int rand = (int) Random.Range(0, 4);
                col1[rand].SetActive(true);
                player.transform.position = col1[rand].transform.position + new Vector3(0,3,0);
                return rand;
            }
            case 2:
            {
                int maxBranch = Mathf.Min(branch+2, 4);
                int rand = (int) Random.Range(0, maxBranch);
                col2[rand].SetActive(true);
                return rand;
            }
            case 3:
            {
                int maxBranch = Mathf.Min(branch+2, 4);
                int rand = (int) Random.Range(0, maxBranch);
                col3[rand].SetActive(true);
                return rand;
            }
            case 4:
            {
                int maxBranch = Mathf.Min(branch+2, 4);
                int rand = (int) Random.Range(0, maxBranch);
                col4[rand].SetActive(true);
                return rand;
            }
            case 5:
            {
                int maxBranch = Mathf.Min(branch+2, 4);
                int rand = (int) Random.Range(0, maxBranch);
                col5[rand].SetActive(true);
                winZone.transform.SetParent(col5[rand].transform, false);
                winZone.transform.localPosition = new Vector3(0.4f, 2.15f,0);
                return rand;
            }
            default:
            {
                return 0;
            }
        }
    }

    public void EndMinigame(bool success)
    {
        resetMap();
        randomMap();
        gameManager.FinishTraining(success, 0); // 0 = Agility Training
    }
}
