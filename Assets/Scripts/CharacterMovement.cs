using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;
    private float m_walkSpeed;
    private float m_turnSpeed;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = 
            RigidbodyConstraints.FreezeRotationX | 
            RigidbodyConstraints.FreezeRotationY | 
            RigidbodyConstraints.FreezeRotationZ;
        m_walkSpeed = 5f;
        m_turnSpeed = 70f;
    }

    void Update()
    {
        //get W/S button pressed
        float v = Input.GetAxis("Vertical") * 1f;

        //since animtor does not have walking backward
        //player will move backward when the S key is pressed
        if (v < 0)
            this.transform.Translate(0, 0, -m_walkSpeed * Time.deltaTime);

        //update animator as well as walk forward
        anim.SetFloat("Forward", v);

        //rotation
        if (Input.GetKey(KeyCode.A))
            this.transform.Rotate(0.0f, -m_turnSpeed * Time.deltaTime, 0.0f);
        else if (Input.GetKey(KeyCode.D))
            this.transform.Rotate(0.0f, m_turnSpeed * Time.deltaTime, 0.0f);
    }
}
