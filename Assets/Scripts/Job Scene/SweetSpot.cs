using UnityEngine;

public class SweetSpot : MonoBehaviour
{
    private float spotPosition;
    private LogMotion logMotion;

    // Start is called before the first frame update
    public void Start()
    {
        logMotion = GetComponentInParent<LogMotion>();
        RandomizePosition();
    }

    // ChopWood successful when hit by axe
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            logMotion.ChopWood(true);
        }
    }

    // Set Sweet Spot to random x-position on log
    public void RandomizePosition()
    {
        spotPosition = Random.Range(0f, 0.5f);
        transform.localPosition = Vector3.right * spotPosition;
    }
}
