using UnityEngine;

public class LogMotion : MonoBehaviour
{
    private JobMinigame jobMinigame;

    public float minPosX = -6f;
    public float maxPosX = 6f;
    public float logSpeed;

    public bool movingRight;
    public bool chopped;

    public float waitTimer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        jobMinigame = GetComponentInParent<JobMinigame>();

        logSpeed = -2f;
        ResetLog();
    }

    // Update is called once per frame
    void Update()
    {
        if (!chopped)
        {
            transform.position += Time.deltaTime * Vector3.right * logSpeed;
            if (movingRight)
            {
                if (transform.position.x > maxPosX)
                {
                    movingRight = false;
                    logSpeed *= -1f;
                }
            }
            else
            {
                if (transform.position.x < minPosX)
                {
                    movingRight = true;
                    logSpeed *= -1f;
                }
            }
        }
        else
        {
            waitTimer -= Time.deltaTime;
            if(waitTimer <= 0f)
            {
                ResetLog();
                waitTimer = 1f;
            }
        }
    }

    // Send log back to starting position, randomize sweet spot
    public void ResetLog()
    {
        transform.position = Vector3.right * maxPosX;
        chopped = false;
        if (logSpeed > 0)
        {
            logSpeed *= -1;
        }
        movingRight = false;
    }

    // Reset log and call ChopLog() from Job Minigame
    public void ChopWood(bool success)
    {
        if (!chopped)
        {
            chopped = true;
            jobMinigame.ChopLog(success);
        }
    }
}
