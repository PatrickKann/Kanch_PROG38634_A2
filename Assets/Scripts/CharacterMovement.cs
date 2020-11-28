using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.constraints =
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        //get W/S button pressed
        float v = Input.GetAxis("Vertical") * 1f;

        //get A/D button pressed
        float h = Input.GetAxis("Horizontal") * 1f;

        //update animator as well as walk forward/turn
        anim.SetFloat("Forward", v);
        anim.SetFloat("Turn", h);

        if (Input.GetKey(KeyCode.Alpha1))
            anim.CrossFade("dance 1", 0.0f);
        else if (Input.GetKey(KeyCode.Alpha2))
            anim.CrossFade("dance 2", 0.0f);
        else if (Input.GetKey(KeyCode.Alpha3))
            anim.CrossFade("dance 3", 0.0f);
        else if (Input.GetKey(KeyCode.Alpha4))
            anim.CrossFade("dance 4", 0.0f);
        else if (Input.GetKey(KeyCode.Alpha5))
            anim.CrossFade("dance 5", 0.0f);
        else if (Input.GetKey(KeyCode.Alpha6))
            anim.CrossFade("dance 6", 0.0f);
    }
}
