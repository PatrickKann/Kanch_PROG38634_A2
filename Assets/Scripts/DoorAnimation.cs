using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    private GameObject player;
    private static bool m_isOpening;

    //to determine whether it is a door or its trigger
    public enum Type { None, Trigger, Door };
    public Type m_type;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //if it should be opening
        //then open slowly until its at 90 degrees
        if (m_isOpening && m_type == Type.Door)
        {
            var rotationVector = transform.rotation.eulerAngles;
            if (++rotationVector.y > 90) rotationVector.y = 90f;
            transform.rotation = Quaternion.Euler(rotationVector);
        }

        //if it should be closing
        //then close slowly until its at 0 degrees
        else if (!m_isOpening && m_type == Type.Door)
        {
            var rotationVector = transform.rotation.eulerAngles;
            if (--rotationVector.y < 0) rotationVector.y = 0f;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if player is in range then open
        if (m_type == Type.Trigger && other.gameObject == player && m_isOpening == false)
        {
            m_isOpening = true;
        }
    }

    void OnTriggerStay(Collider col)
    {
        //if player is in range then open
        if (m_type == Type.Trigger && col.gameObject == player && m_isOpening == false)
        {
            m_isOpening = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if player is NOT in range then close
        if (m_type == Type.Trigger && other.gameObject == player && m_isOpening == true)
        {
            m_isOpening = false;
        }
    }
}
