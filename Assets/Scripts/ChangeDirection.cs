using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirection : MonoBehaviour
{
    private Animator anim;
    public float m_speed;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetFloat("Forward", m_speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            Debug.Log("Turning");
            StartCoroutine("Turn");
        }
    }

    IEnumerator Turn()
    {
        anim.SetFloat("Turn", 1);
        yield return new WaitForSeconds(1.75f);
        anim.SetFloat("Turn", 0);
        StopAllCoroutines();
    }
}
