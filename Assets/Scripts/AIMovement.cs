using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Aspect;

public class AIMovement : MonoBehaviour
{
    //To define what type of AI they are
    public enum AIType { None, Pedestrian, Car };
    public AIType m_type;

    //Movement variables
    private float m_speed;

    public float m_carSpeed;

    //End points
    private List<GameObject> roadEnds = new List<GameObject>();
    private List<GameObject> roadStarts = new List<GameObject>();
    private List<GameObject> sideEnds = new List<GameObject>();
    private List<GameObject> sideStarts = new List<GameObject>();

    //For Pedestrian
    private Animator anim;
    private Rigidbody rb;

    //toggle
    public bool canMove = true;

    void Start()
    {
        //determine their speed based on their AI type
        switch (m_type)
        {
            case AIType.Car:
                m_speed = m_carSpeed;
                break;
            case AIType.Pedestrian:
                m_speed = 0.5f;
                break;
            default:
                m_speed = 0;
                break;
        }

        //add the ends & beginnings of the roads to the script
        //in order to make AI movement more dynamic 
        foreach (var obj in GameObject.FindGameObjectsWithTag("EndOfRoad"))
        {
            if (obj.name.Contains("RoadEnd")) roadEnds.Add(obj);
            else if (obj.name.Contains("RoadStart")) roadStarts.Add(obj);
        }

        foreach (var obj in GameObject.FindGameObjectsWithTag("EndOfSidewalk"))
        {
            if (obj.name.Contains("SideEnd")) sideEnds.Add(obj);
            else if (obj.name.Contains("SideStart")) sideStarts.Add(obj);
        }

        //Pedestrains need animator and rigidbody to walk 
        if (m_type == AIType.Pedestrian)
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            if (rb != null)
                rb.constraints =
                    RigidbodyConstraints.FreezeRotationX |
                    RigidbodyConstraints.FreezeRotationY |
                    RigidbodyConstraints.FreezeRotationZ;
        }

        else if (m_type == AIType.Car)
        {
            this.gameObject.AddComponent(typeof(Aspect));
            this.gameObject.GetComponent<Aspect>().aspectType = AspectTypes.ENEMY;

            this.gameObject.AddComponent(typeof(Perspective));

            this.gameObject.AddComponent(typeof(Touch));
        }
    }

    void Update()
    {
        if (canMove)
        {
            //Cars needs to move forward as if it's driving forward
            if (m_type == AIType.Car)
                this.transform.Translate(0, 0, m_speed * Time.deltaTime);

            //Pedestrains need to update their animator in order to move forward
            else if (m_type == AIType.Pedestrian)
                anim.SetFloat("Forward", m_speed);
        }
        else if (!canMove)
        {
            StartCoroutine("Pause");
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (m_type == AIType.Car)
        {
            //if a car hits the end of a road, 
            //then it will be teleported to a random start location and continue driving 
            if (col.gameObject.name.Contains("RoadEnd"))
            {
                GameObject randomStart = roadStarts[(Random.Range(0, roadStarts.Count))];
                this.transform.position = randomStart.transform.position;
                this.transform.rotation = randomStart.transform.rotation;
            }

            //if a car hits another car/a pedestrain, 
            //then it will restart at a random start location and continue driving 
            else if (col.gameObject.name.Contains("AI_Car") || col.gameObject.name.Contains("AI_Ped"))
            {
                GameObject randomStart = roadStarts[(Random.Range(0, roadStarts.Count))];
                this.transform.position = randomStart.transform.position;
                this.transform.rotation = randomStart.transform.rotation;
            }
        }

        else if (m_type == AIType.Pedestrian)
        {
            //if a pedestrain hits the end of a sidewalk,
            //then it will restart at a random start location 
            if (col.gameObject.name.Contains("SideEnd"))
            {
                GameObject randomStart = sideStarts[(Random.Range(0, sideStarts.Count))];
                this.transform.position = randomStart.transform.position;
                this.transform.rotation = randomStart.transform.rotation;
            }

            //if a pedestrain gets hit by a car/another pedestrain,
            //then it will restart at a random start location 
            else if (col.gameObject.name.Contains("AI_Car") || col.gameObject.name.Contains("AI_Ped"))
            {
                GameObject randomStart = sideStarts[(Random.Range(0, sideStarts.Count))];
                this.transform.position = randomStart.transform.position;
                this.transform.rotation = randomStart.transform.rotation;
            }
        }
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1.5f);
        canMove = true;
        GameObject stopsign = GameObject.FindGameObjectWithTag("Stopsign");

        this.GetComponent<Perspective>().enabled = false;
        yield return new WaitForSeconds(2f);
        this.GetComponent<Perspective>().enabled = true;

        StopAllCoroutines();
    }
}
