using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public GameObject mapUI;
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 compassAngle = new Vector3();
        compassAngle.z = target.transform.eulerAngles.y;
        mapUI.transform.eulerAngles = compassAngle;
    }
}
