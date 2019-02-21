using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Animator trapAnim;
    public void TrapActivated()
    {
        trapAnim.enabled = true;
        Destroy(gameObject, 2);
    }
}
