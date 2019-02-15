using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharismaTrainingMinigame : MonoBehaviour
{
    // CONSTANTS
    //private readonly float MAXSLIDERVALUE = 250f;
    private readonly float MINSLIDERVALUE = -250f;
    private readonly float MAXSWEETSPOTPOS = 225f;
    private readonly float MINSWEETSPOTPOS = -150f;

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
    public float loveTimer = 3f;
    public float failTimer = 3f;
    private bool activated;
    public TextMeshProUGUI loveTimerText;


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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            loveLevelSlider.value += increaseAmount;
        }
        if ((loveLevelSlider.value < sweetSpotPosition + (sweetSpotHeight / 2)) &&
           (loveLevelSlider.value > sweetSpotPosition - (sweetSpotHeight / 2)))
        {
            activated = true;
            sweetSpotImage.color = Color.green;
            loveTimer -= Time.deltaTime;
            loveTimerText.text = loveTimer.ToString("0.0");
            if (loveTimer < 0f)
            {
                EndMinigame(true);
            }
        }
        else
        {
            sweetSpotImage.color = Color.red;
            if (activated)
            {
                failTimer -= Time.deltaTime;
                if(failTimer < 0f)
                {
                    EndMinigame(false);
                }
            }
        }
    }

    // Set sweeet spot to random x-position between min and max
    public void RandomizeSweetSpot()
    {
        sweetSpotPosition = Random.Range(MINSWEETSPOTPOS, MAXSWEETSPOTPOS);
        sweetSpotTransform.localPosition = Vector3.right * sweetSpotPosition;
    }

    // Finish training minigame and increase charisma if successful
    public void EndMinigame(bool success)
    {
        if (success)
        {
            gameManager.charismaStat++;
        }
        gameManager.FinishTraining(1); // 1 = Charisma Training
    }

    // Reset to starting values
    public void ResetMinigame()
    {
        loveLevelSlider.value = MINSLIDERVALUE;
        loveTimer = 3f;
        failTimer = 3f;
        activated = false;
        loveTimerText.text = loveTimer.ToString("0.0");
        sweetSpotImage.color = new Color(0f, 255f, 0f, 100f);
        RandomizeSweetSpot();
    }
}
