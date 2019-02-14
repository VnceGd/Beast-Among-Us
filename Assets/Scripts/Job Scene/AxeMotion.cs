using UnityEngine;

public class AxeMotion : MonoBehaviour
{
    public float axeSpeed = 3f;

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y");

        if(Mathf.Abs(mouseY) > 0f)
        {
            if (transform.position.y > 3f)
            {
                transform.position = new Vector3(transform.position.x, 3f, transform.position.z);
            }
            else if (transform.position.y < 0f)
            {
                transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            }
            else
            {
                transform.position += Time.deltaTime * mouseY * Vector3.up * axeSpeed;
            }
        }
    }
}
