using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly float STARTMOVESPEED = 5f;

    private GameObject manager;
    private GameManager gameManager;

    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;
    public bool werewolfMode;

    private Rigidbody playerBody;

    public GameObject trap;

    public GameObject decoy;

    public GameObject forkAndKnife;

    public float fakTimer;

    public float fakTime = 3;

    public bool fakActive;

    // Start is called before the first frame update
    public void Start()
    {
        manager = GameObject.Find("Game Manager");
        gameManager = manager.GetComponent<GameManager>();

        playerBody = GetComponent<Rigidbody>();

        UpdateSpeedStat();

        forkAndKnife.SetActive(false);
        fakTimer = fakTime;
        fakActive = false;
    }

    // Update is called once per frame
    private void Update()
    {
        float h_input = Input.GetAxis("Horizontal");
        float v_input = Input.GetAxis("Vertical");
        Vector3 moveVelocity = Vector3.zero;

        if (Mathf.Abs(h_input) > 0f)
        {
            moveVelocity += transform.right * Time.deltaTime * h_input * moveSpeed;
        }
        if (Mathf.Abs(v_input) > 0f)
        {
            moveVelocity += transform.forward * Time.deltaTime * v_input * moveSpeed;
        }
        playerBody.MovePosition(transform.position + moveVelocity);

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Time.deltaTime * Vector3.down * rotateSpeed);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Time.deltaTime * Vector3.up * rotateSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            
        }

        if (Input.GetKeyDown(KeyCode.F)) 
        {
            GameObject newTrap = Instantiate(trap, new Vector3(transform.position.x, 0.01f, transform.position.z), trap.transform.rotation);
            newTrap.transform.SetParent(transform.parent);
        }

        if (Input.GetKeyDown(KeyCode.C)) 
        {
            GameObject newDecoy = Instantiate(decoy, transform.position, transform.rotation);
            newDecoy.transform.SetParent(transform.parent);
        }

        if(fakActive == true)
        {
            fakTimer -= Time.deltaTime;
            if(fakTimer <= 0)
            {
                forkAndKnife.SetActive(false);
                fakActive = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            forkAndKnife.SetActive(true);
            fakActive = true;
        }
    }

    // Open Inventory Panel
    public void OpenInventory()
    {

    }

    // Update player speed based on stat
    public void UpdateSpeedStat()
    {
        if(gameManager)
        {
            moveSpeed = STARTMOVESPEED + gameManager.speedStat;
        }
    }
}
