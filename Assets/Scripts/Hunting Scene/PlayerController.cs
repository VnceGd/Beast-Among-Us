﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly float STARTMOVESPEED = 5f;

    private GameObject manager;
    public GameManager gameManager;

    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;
    public bool werewolfMode;

    private Rigidbody playerBody;

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

        UpdateSpeedStat();

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

        if (Mathf.Abs(h_input) > 0f)
        {
            moveVelocity += transform.right * Time.deltaTime * h_input * moveSpeed;
            beastAnim.SetBool("IsMoving", true);
        }
        if (Mathf.Abs(v_input) > 0f)
        {
            moveVelocity += transform.forward * Time.deltaTime * v_input * moveSpeed;
            beastAnim.SetBool("IsMoving", true);
        }
        if(Mathf.Abs(h_input) == 0f && Mathf.Abs(v_input) == 0f)
        {
            beastAnim.SetBool("IsMoving", false);
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

        if (Input.GetKeyDown(KeyCode.Space))
        {

        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (gameManager.bearTrapCount > 0)
            {
                GameObject newTrap = Instantiate(trap, new Vector3(transform.position.x, 0.01f, transform.position.z), trap.transform.rotation);
                newTrap.transform.SetParent(transform.parent);
                gameManager.bearTrapCount--;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (gameManager.decoyCount > 0)
            {
                GameObject newDecoy = Instantiate(decoy, transform.position, transform.rotation);
                newDecoy.transform.SetParent(transform.parent);
                gameManager.decoyCount--;
            }
        }

        if (shieldDown)
        {
            shieldTimer += Time.deltaTime;
            if (shieldTimer > shieldCooldown)
            {
                shieldDown = false;
                shieldTimer = 0f;
            }
        }

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

    // Open Inventory Panel
    public void OpenInventory()
    {

    }

    // Update player speed based on stat
    public void UpdateSpeedStat()
    {
        if (gameManager)
        {
            moveSpeed = STARTMOVESPEED + gameManager.speedStat;
        }
    }
}
