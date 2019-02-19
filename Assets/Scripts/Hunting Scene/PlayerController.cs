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

    // Start is called before the first frame update
    public void Start()
    {
        manager = GameObject.Find("Game Manager");
        gameManager = manager.GetComponent<GameManager>();

        playerBody = GetComponent<Rigidbody>();

        UpdateSpeedStat();
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
    }

    // Update player speed based on stat
    public void UpdateSpeedStat()
    {
        moveSpeed = STARTMOVESPEED + gameManager.speedStat;
    }
}
