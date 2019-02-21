using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly float STARTMOVESPEED = 5f;
    private readonly float STARTFOV = 60f;

    private GameObject manager;
    public GameManager gameManager;

    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;
    public bool werewolfMode;

    public Transform playerTransform;
    private Rigidbody playerBody;
    private Camera playerCamera;

    public GameObject trap;

    public GameObject decoy;

    public GameObject fork;

    public GameObject knife;

    public float fakTimer;

    public float fakTime = 3;

    public bool fakActive;

    public bool shieldDown;

    public float shieldCooldown = 2f;

    public float shieldTimer;

    public Animator beastAnim;

    // Start is called before the first frame update
    public void Start()
    {
        manager = GameObject.Find("Game Manager");
        gameManager = manager.GetComponent<GameManager>();

        playerBody = GetComponent<Rigidbody>();
        playerCamera = Camera.main;

        UpdateStats();

        fork.SetActive(false);
        knife.SetActive(false);
        fakTimer = fakTime;
        fakActive = false;
    }

    // Update is called once per frame
    private void Update()
    {
        float h_input = Input.GetAxis("Horizontal");
        float v_input = Input.GetAxis("Vertical");
        Vector3 moveVelocity = Vector3.zero;

        // Move player
        if (Mathf.Abs(h_input) > 0f)
        {
            moveVelocity += transform.right * Time.deltaTime * h_input * moveSpeed;
            beastAnim.SetBool("IsMoving", true);
            if (h_input > 0f)
            {
                if (v_input > 0f)
                {
                    playerTransform.localEulerAngles = Vector3.up * 45f;
                }
                else if (v_input < 0f)
                {
                    playerTransform.localEulerAngles = Vector3.up * 135f;
                }
                else
                {
                    playerTransform.localEulerAngles = Vector3.up * 90f;
                }
            }
            else
            {
                if (v_input > 0f)
                {
                    playerTransform.localEulerAngles = Vector3.up * -45f;
                }
                else if (v_input < 0f)
                {
                    playerTransform.localEulerAngles = Vector3.up * -135f;
                }
                else
                {
                    playerTransform.localEulerAngles = Vector3.up * -90f;
                }
            }
        }
        if (Mathf.Abs(v_input) > 0f)
        {
            moveVelocity += transform.forward * Time.deltaTime * v_input * moveSpeed;
            beastAnim.SetBool("IsMoving", true);
            if (v_input > 0f)
            {
                if (h_input > 0f)
                {
                    playerTransform.localEulerAngles = Vector3.up * 45f;
                }
                else if (h_input < 0f)
                {
                    playerTransform.localEulerAngles = Vector3.up * -45f;
                }
                else
                {
                    playerTransform.localEulerAngles = Vector3.up;
                }
            }
            else
            {
                if (h_input > 0f)
                {
                    playerTransform.localEulerAngles = Vector3.up * 135f;
                }
                else if (h_input < 0f)
                {
                    playerTransform.localEulerAngles = Vector3.up * -135f;
                }
                else
                {
                    playerTransform.localEulerAngles = Vector3.up * 180f;
                }
            }
        }
        if(Mathf.Abs(h_input) < Mathf.Epsilon && Mathf.Abs(v_input) < Mathf.Epsilon)
        {
            beastAnim.SetBool("IsMoving", false);
        }
        playerBody.MovePosition(transform.position + moveVelocity);

        // Rotate player
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Time.deltaTime * Vector3.down * rotateSpeed);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Time.deltaTime * Vector3.up * rotateSpeed);
        }

        // Use Trap 
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (gameManager.bearTrapCount > 0)
            {
                GameObject newTrap = Instantiate(trap, new Vector3(transform.position.x, 0.01f, transform.position.z), trap.transform.rotation);
                newTrap.transform.SetParent(transform.parent);
                gameManager.bearTrapCount--;
            }
        }

        // Use Decoy
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (gameManager.decoyCount > 0)
            {
                GameObject newDecoy = Instantiate(decoy, transform.position, transform.rotation);
                newDecoy.transform.SetParent(transform.parent);
                gameManager.decoyCount--;
            }
        }

        // Shield Cooldown
        if (shieldDown)
        {
            shieldTimer += Time.deltaTime;
            if (shieldTimer > shieldCooldown)
            {
                shieldDown = false;
                shieldTimer = 0f;
            }
        }

        // Fork and Knife Cooldown
        if (fakActive == true)
        {
            fakTimer -= Time.deltaTime;
            if (fakTimer <= 0)
            {
                fakTimer = fakTime;
                fork.SetActive(false);
                knife.SetActive(false);
                fakActive = false;
            }
        }

        // Activate Fork and Knife
        if (Input.GetKeyDown(KeyCode.R) && !fakActive)
        {
            if (gameManager.forkAndKnifeCount > 0)
            {
                fork.SetActive(true);
                knife.SetActive(true);
                fakActive = true;
                gameManager.forkAndKnifeCount--;
            }
        }
    }

    // Update player speed based on stat
    public void UpdateStats()
    {
        if (gameManager)
        {
            moveSpeed = STARTMOVESPEED + gameManager.speedStat;
        }
        if (playerCamera)
        {
            playerCamera.fieldOfView = STARTFOV + (gameManager.awarenessStat * 5);
        }
    }
}
