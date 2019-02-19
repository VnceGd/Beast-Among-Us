using UnityEngine;
using UnityEngine.UI;

public class CharismaTrainingMinigame : MonoBehaviour
{
    // CONSTANTS
    private readonly float MINSLIDERVALUE = -250f;
    private readonly float MAXSWEETSPOTPOS = 225f;
    private readonly float MINSWEETSPOTPOS = -150f;
    private readonly float MAXPROGRESS = 4f;
    private readonly float STARTPROGRESS = 2f;

    private GameObject manager;
    private GameManager gameManager;

    // Sweet Spot
    public RectTransform sweetSpotTransform;
    public float sweetSpotPosition;
    public Image sweetSpotImage;
    public float sweetSpotHeight = 50f;

    // Love Level Slider
    public Slider loveLevelSlider;
    public float increaseAmount = 10f;
    public float decaySpeed = 20f;

    public Slider progressBar;
    public float progressIncreaseRate = 1f;
    public float progressDecreaseRate = 0.3f;

    // Start is called before the first frame update
    public void Start()
    {
        manager = GameObject.Find("Game Manager");
        gameManager = manager.GetComponent<GameManager>();

        ResetMinigame();
    }

    // Update is called once per frame
    private void Update()
    {
        loveLevelSlider.value -= Time.deltaTime * decaySpeed;
        progressBar.value -= Time.deltaTime * progressDecreaseRate;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            loveLevelSlider.value += increaseAmount;
        }
        if ((loveLevelSlider.value < sweetSpotPosition + (sweetSpotHeight / 2)) &&
           (loveLevelSlider.value > sweetSpotPosition - (sweetSpotHeight / 2)))
        {
            sweetSpotImage.color = Color.green;
            progressBar.value += Time.deltaTime * progressIncreaseRate;
            if (progressBar.value >= MAXPROGRESS)
            {
                EndMinigame(true);
            }
        }
        else
        {
            sweetSpotImage.color = Color.red;
            if (progressBar.value <= 0f)
            {
                EndMinigame(false);
            }
        }
    }

    // Set sweeet spot to random x-position between min and max
    public void RandomizeSweetSpot()
    {
        sweetSpotPosition = Random.Range(MINSWEETSPOTPOS, MAXSWEETSPOTPOS);
        sweetSpotTransform.localPosition = Vector3.right * sweetSpotPosition;
    }

    // Reset to starting values
    public void ResetMinigame()
    {
        loveLevelSlider.value = MINSLIDERVALUE;
        progressBar.value = STARTPROGRESS;
        RandomizeSweetSpot();
    }

    // Finish training minigame and increase charisma if successful
    public void EndMinigame(bool success)
    {
        //if (success)
        //{
        //    gameManager.charismaStat++;
        //}
        ResetMinigame();
        gameManager.FinishTraining(success, 1); // 1 = Charisma Training
    }
}
