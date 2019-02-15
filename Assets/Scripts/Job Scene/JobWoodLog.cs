using UnityEngine;

public class JobWoodLog : MonoBehaviour
{
    private LogMotion logMotion;

    // Start is called before the first frame update
    public void Start()
    {
        logMotion = GetComponentInParent<LogMotion>();
    }

    // ChopWood unsuccessful when hit by axe
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            logMotion.ChopWood(false);
        }
    }
}
