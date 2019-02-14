using UnityEngine;
using UnityEngine.UI;

public class TrainingMinigame : MonoBehaviour
{
    private GameObject manager;
    private GameManager gameManager;

    private Camera mainCamera;

    public int treeHitPoints = 20;
    public Slider treeHitPointSlider;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Game Manager");
        gameManager = manager.GetComponent<GameManager>();

        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                treeHitPoints--;
                treeHitPointSlider.value = treeHitPoints;
                if (treeHitPoints <= 0)
                {
                    ResetMinigame();
                    gameManager.FinishTraining();
                }
            }
        }
    }

    // Reset Minigame
    public void ResetMinigame()
    {
        treeHitPoints = 20;
        treeHitPointSlider.value = treeHitPoints;
    }
}
