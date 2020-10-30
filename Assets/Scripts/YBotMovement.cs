using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YBotMovement : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;
    private AudioSource aud;

    public AudioClip walkSound;

    public bool isHoldingGun;

    [Range(0.5f, 10.0f)]
    public float walkSpeed = 2.0f;

    [Range(1f, 100.0f)]
    public float turnSpeed = 50.0f;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        aud = GetComponent<AudioSource>();

        rb.constraints =
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        #region Movement
        float v = Input.GetAxis("Vertical") * 1f;
        float h = Input.GetAxis("Horizontal") * 1f;

        anim.SetFloat("Forward", v);
        anim.SetFloat("Turn", h);

        this.transform.Translate(0, 0, v * walkSpeed * Time.deltaTime);
        this.transform.Rotate(new Vector3(0, h * turnSpeed * Time.deltaTime, 0));
        #endregion

        if (v != 0)
        {
            if (walkSound && !aud.clip) aud.clip = walkSound;
            if (!aud.isPlaying) aud.Play();
        }
    }
}
