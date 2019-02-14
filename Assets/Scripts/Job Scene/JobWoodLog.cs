using UnityEngine;

public class JobWoodLog : MonoBehaviour
{
    private LogMotion logMotion;

    // Start is called before the first frame update
    public void Start()
    {
        logMotion = GetComponentInParent<LogMotion>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            logMotion.ChopWood(false);
        }
    }
}
