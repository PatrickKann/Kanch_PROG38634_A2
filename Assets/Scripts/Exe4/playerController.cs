using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    public float speed;

    Rigidbody rb;
    Animator anim;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        float ver = Input.GetAxis("Vertical") * -speed;
        float hor = Input.GetAxis("Horizontal") * speed;
        Vector3 mov = new Vector3(ver, 0, hor) * Time.deltaTime;
        Vector3 newPos = transform.position + mov;
        rb.MovePosition(newPos);

        if (ver == 0) anim.SetFloat("Forward", 0f);
        else if (ver < 0) anim.SetFloat("Forward", 1f);
        else if (ver > 0) anim.SetFloat("Forward", -1f);

        if (hor == 0) anim.SetFloat("Turn", 0f);
        else if (hor < 0) anim.SetFloat("Turn", -1f);
        else if (hor > 0) anim.SetFloat("Turn", 1f);

        Transform tank = transform.GetChild(0).GetComponent<Transform>();
        if (mov.magnitude > 0)
            tank.rotation = Quaternion.LookRotation(mov, Vector3.up);
    }

}