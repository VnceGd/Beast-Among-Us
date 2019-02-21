using UnityEngine;

public class AwarenessTrainingMinigame : MonoBehaviour
{
    private readonly Vector3 STARTPOSITION = Vector3.up * 2f;
    private readonly int STARTSWAPCOUNT = 4;

    private GameManager gameManager;

    public GameObject[] cubes;
    public GameObject cubeArray;

    public bool gameStarted;
    public float countdownTimer;
    public float countdownDuration = 1f;

    public bool swapping;
    public float swapTimer;
    public float swapDuration = 1f;
    public int swapCount = 5;

    public GameObject cubeSwapping1;
    public GameObject cubeSwapping2;
    public Vector3 cube1Position;
    public Vector3 cube2Position;

    public GameObject squirrel;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        ResetMinigame();

        UpdateSwapCount();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            if (swapping)
            {
                if (cube1Position.x < cube2Position.x)
                {
                    cubeSwapping1.transform.position += Vector3.right * Time.deltaTime * Vector3.Distance(cube1Position, cube2Position);
                    cubeSwapping2.transform.position += -1 * Vector3.right * Time.deltaTime * Vector3.Distance(cube2Position, cube1Position);
                }
                else
                {
                    cubeSwapping1.transform.position += -1 * Vector3.right * Time.deltaTime * Vector3.Distance(cube1Position, cube2Position);
                    cubeSwapping2.transform.position += Vector3.right * Time.deltaTime * Vector3.Distance(cube2Position, cube1Position);
                }

                swapTimer += Time.deltaTime;
                if (swapTimer > swapDuration)
                {
                    swapping = false;
                    swapTimer = 0f;
                }
            }
            else
            {
                if (swapCount > 0)
                {
                    SwapRandom();
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Ray ray = GameObject.Find("Menu Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

                        if (Physics.Raycast(ray, out RaycastHit hit))
                        {
                            if (hit.transform == cubes[1].transform)
                            {
                                EndMinigame(true);
                            }
                            else
                            {
                                EndMinigame(false);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            countdownTimer += Time.deltaTime;
            cubeArray.transform.position += Vector3.down * Time.deltaTime * 2;
            if (countdownTimer > countdownDuration)
            {
                gameStarted = true;
                squirrel.transform.parent = cubes[1].transform;
            }
        }
    }

    // Reset variables and transforms to starting values
    public void ResetMinigame()
    {
        gameStarted = false;
        countdownTimer = 0f;
        cubeArray.transform.position = STARTPOSITION;
        cubes[0].transform.position = STARTPOSITION + (Vector3.right * -4f);
        cubes[1].transform.position = STARTPOSITION;
        cubes[2].transform.position = STARTPOSITION + (Vector3.right * 4f);
        squirrel.transform.position = Vector3.zero;
        squirrel.transform.parent = transform;
    }

    // Reset and exit minigame
    public void EndMinigame(bool success)
    {
        ResetMinigame();
        gameManager.FinishTraining(success, 2);
    }

    // Choose two random cubes to swap
    public void SwapRandom()
    {
        int rand = Random.Range(0, cubes.Length);

        cubeSwapping1 = cubes[rand];
        cube1Position = cubeSwapping1.transform.position;
        cubeSwapping2 = cubes[(rand + Random.Range(1, cubes.Length)) % cubes.Length];
        cube2Position = cubeSwapping2.transform.position;
        swapping = true;
        swapCount--;
    }

    // Increase swap count based on current Awareness stat
    public void UpdateSwapCount()
    {
        swapCount = STARTSWAPCOUNT + gameManager.awarenessStat;
    }
}
