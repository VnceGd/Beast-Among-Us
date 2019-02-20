using UnityEngine;
using UnityEngine.AI;

public class HunterNavigation : MonoBehaviour
{
    private HuntingMinigame huntingMinigame;

    private NavMeshAgent myAgent;

    public GameObject pitchfork;

    public bool attacking;
    public bool attackReady;
    public float attackTimer;
    public float attackDuration = 1f;
    public float cooldownTimer;
    public float attackCooldown = 3f;

    // Start is called before the first frame update
    public void Start()
    {
        huntingMinigame = GameObject.Find("Hunting Minigame").GetComponent<HuntingMinigame>();

        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!attackReady)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer > attackCooldown)
            {
                attackReady = true;
                cooldownTimer = 0f;
            }
        }
        else
        {
            if (myAgent.remainingDistance < 5f)
            {
                Attack();
            }
        }

        if (attacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackDuration)
            {
                pitchfork.transform.Rotate(Vector3.back * 90f);
                attacking = false;
                attackTimer = 0f;
            }
            myAgent.destination = GameObject.Find("Player").transform.position + (transform.forward * 2f);
        }
        else
        {
            myAgent.destination = GameObject.Find("Player").transform.position;
        }
    }

    // Play attack animation
    public void Attack()
    {
        pitchfork.transform.Rotate(Vector3.forward * 90f);
        attacking = true;
        attackReady = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (attacking)
            {
                if (gameManager.shieldCount > 0)
                {
                    gameManager.shieldCount--;
                }
                else
                {
                    huntingMinigame.EndMinigame();
                }
            }
            else
            {
                huntingMinigame.EatAnimal();
                Destroy(gameObject);
            }
        }
    }
}
