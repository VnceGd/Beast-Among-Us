using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgilityMovement : MonoBehaviour
{
    public float speed = 5;

    public float jumpForce = 10;

    private Rigidbody playerBody;

    private bool isInAir;

    public Animator beastAnim;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        isInAir = false;
    }

    // Update is called once per frame
    void Update()
    {
        float h_input = Input.GetAxis("Horizontal");
        Vector3 moveVelocity = Vector3.zero;

        if(Mathf.Abs(h_input) > 0f)
        {
            moveVelocity += Vector3.right * Time.deltaTime * h_input * speed;
            if(h_input > 0f)
            {
                Debug.Log("Face Right");
                transform.rotation = Quaternion.Euler(0, 0, 0);
                beastAnim.SetBool("IsMoving", true);
            }
            else if(h_input < 0f)
            {
                Debug.Log("Face Right");
                transform.rotation = Quaternion.Euler(0, 180, 0);
                beastAnim.SetBool("IsMoving", true);
            }
        }
        else
        {
            beastAnim.SetBool("IsMoving", false);
        }

        if (playerBody.velocity.y < Mathf.Epsilon)
        {
            isInAir = false;
        }

        if (Input.GetKeyDown("space") && !isInAir)
        {
            isInAir = true;
            Debug.Log("jump");
            playerBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        playerBody.MovePosition(transform.position + moveVelocity);
    }
}
