using UnityEngine;
using UnityEngine.AI;

public class HunterNavigation : MonoBehaviour
{
    private NavMeshAgent myAgent;

    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        myAgent.destination = GameObject.Find("Player").transform.position;
    }
}
