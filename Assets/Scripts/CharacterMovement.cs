using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;

    [Range(0.5f, 10.0f)]
    public float m_walkSpeed = 6.0f;

    [Range(1f, 100.0f)]
    public float m_turnSpeed = 75.0f;

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

        //update animator as well as walk forward
        anim.SetFloat("Forward", v);

        //since animtor does not have walking backward
        //player will move backward when the S key is pressed
        if (Input.GetKey(KeyCode.S))
            this.transform.Translate(0, 0, -m_walkSpeed * Time.deltaTime);

        //rotation
        if (Input.GetKey(KeyCode.A))
            this.transform.Rotate(0.0f, -m_turnSpeed * Time.deltaTime, 0.0f);
        else if (Input.GetKey(KeyCode.D))
            this.transform.Rotate(0.0f, m_turnSpeed * Time.deltaTime, 0.0f);
    }
}
