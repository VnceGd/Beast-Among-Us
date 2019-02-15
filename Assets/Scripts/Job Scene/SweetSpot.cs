using UnityEngine;

public class SweetSpot : MonoBehaviour
{
    private LogMotion logMotion;

    // Start is called before the first frame update
    public void Start()
    {
        logMotion = GetComponentInParent<LogMotion>();
    }

    // ChopWood successful when hit by axe
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            logMotion.ChopWood(true);
        }
    }
}
