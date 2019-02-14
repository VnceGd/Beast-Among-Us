using UnityEngine;
using UnityEngine.UI;

public class TreeCut : MonoBehaviour
{
    private GameObject manager;
    private GameManager gameManager;

    public int treeHitPoints = 20;
    public Slider treeHitPointSlider;

    private Camera mainCamera;

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
        if(Input.GetButtonDown("Fire1")) 
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                treeHitPoints--;
                treeHitPointSlider.value = treeHitPoints;
                if (treeHitPoints <= 0)
                {
                    gameManager.FinishTraining();
                }
            }
        }
    }
}
