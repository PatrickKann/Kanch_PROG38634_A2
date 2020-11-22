using UnityEngine;

public class Perspective : Sense
{
    public int fieldOfView = 20;//45;
    public int viewDistance = 5;//100;


    public Transform playerTransform;
    private Vector3 rayDirection;

    protected override void Initialize()
    {
        if (aspectName == Aspect.AspectTypes.ENEMY) //meaning that this is a car
            playerTransform = GameObject.FindGameObjectWithTag("Stopsign").transform;
        else if (aspectName == Aspect.AspectTypes.PLAYER) //else it is a pedestrian 
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void UpdateSense()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= detectionRate)
        {
            DetectAspect();
        }
    }

    //Detect perspective field of view for the AI Character
    void DetectAspect()
    {
        RaycastHit hit;
        rayDirection = playerTransform.position - transform.position;

        if ((Vector3.Angle(rayDirection, transform.forward)) < fieldOfView)
        {
            // Detect if player is within the field of view
            if (Physics.Raycast(transform.position, rayDirection, out hit, viewDistance))
            {
                Aspect aspect = hit.collider.GetComponent<Aspect>();
                if (aspect != null)
                {
                    //Check the aspect
                    if (aspect.aspectType == aspectName)
                    {
                        if (this.gameObject.tag == "Car")
                            this.gameObject.GetComponent<AIMovement>().canMove = false;
                        else if (this.gameObject.tag == "Pedestrian")
                            Debug.Log("found player");
                    }
                }
            }
        }
    }

    /// <summary>
    /// Show Debug Grids and obstacles inside the editor
    /// </summary>
    void OnDrawGizmos()
    {
        if (playerTransform == null) 
        {
            return;
        }

        Debug.DrawLine(transform.position, playerTransform.position, Color.red);

        Vector3 frontRayPoint = transform.position + (transform.forward * viewDistance);

        Debug.DrawLine(transform.position, frontRayPoint, Color.blue);
    }
}
