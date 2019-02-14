using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float startRotationX = 90f;
    public float rotationSpeed = 1f;

    private Light lt;

    private GameObject player;
    private PlayerMovement playerScript;

    // Start is called before the first frame update
    public void Start()
    {
        transform.localEulerAngles = new Vector3(startRotationX, 0f, 0f);
        lt = GetComponent<Light>();
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(Vector3.right * rotationSpeed);
        if (transform.localEulerAngles.x > 200f)
        {
            NightTime();
        }
        else if(transform.localEulerAngles.x > 0f)
        {
            DayTime();
        }
    }

    // Reveal werewolf traits on player
    public void NightTime()
    {
        if(!playerScript.werewolfMode)
        {
            playerScript.ToggleWerewolfMode();
        }
        if(lt.intensity > 0f)
        {
            lt.intensity -= Time.deltaTime;
        }
    }

    // Remove werewolf traits from player
    public void DayTime()
    {
        if(playerScript.werewolfMode)
        {
            playerScript.ToggleWerewolfMode();
        }
        if(lt.intensity < 1f)
        {
            lt.intensity += Time.deltaTime;
        }
    }
}
