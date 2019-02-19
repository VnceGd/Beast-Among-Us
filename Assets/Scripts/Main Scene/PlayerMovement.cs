using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject manager;
    private GameManager gameManager;

    public float moveSpeed = 5f;
    public bool werewolfMode;

    private Rigidbody playerBody;
    private GameObject werewolfTraits;

    // Start is called before the first frame update
    public void Start()
    {
        manager = GameObject.Find("Game Manager");
        gameManager = manager.GetComponent<GameManager>();

        playerBody = GetComponent<Rigidbody>();
        werewolfTraits = GameObject.Find("Werewolf Traits");
        werewolfTraits.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        float h_input = Input.GetAxis("Horizontal");
        float v_input = Input.GetAxis("Vertical");
        Vector3 moveVelocity = Vector3.zero;

        if (Mathf.Abs(h_input) > 0f)
        {
            moveVelocity += Vector3.right * Time.deltaTime * h_input * moveSpeed;
        }
        if (Mathf.Abs(v_input) > 0f)
        {
            moveVelocity += Vector3.forward * Time.deltaTime * v_input * moveSpeed;
        }
        playerBody.MovePosition(transform.position + moveVelocity);
    }

    // Reveal or remove werewolf traits
    public void ToggleWerewolfMode()
    {
        if (werewolfMode)
        {
            werewolfTraits.SetActive(false);
            werewolfMode = false;
        }
        else
        {
            werewolfTraits.SetActive(true);
            werewolfMode = true;
        }
    }
}
