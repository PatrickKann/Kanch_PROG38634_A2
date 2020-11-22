#region usings

using System.Linq;
using UnityEngine;
using UnityEngine.AI;

#endregion

public class AIInteractions : MonoBehaviour 
{
    public enum Type { Follow, Hide, Drive, Dance };

    public Type AIType;
    public Transform targetPos;

    public int danceIndex;
    public float movementSpeed = 0.5f;
    public float rotationSpeed = 10f;
    public float targetPositionTolerance;

    private Animator anim;

    void Start () 
    {
        anim = GetComponent<Animator>();
    }
	
	void Update () 
    {
        float distance = Vector3.Distance(targetPos.position, transform.position);

        Quaternion targetRotation = Quaternion.LookRotation(targetPos.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (AIType == Type.Follow)
        {
            anim.SetFloat("Forward", (distance > targetPositionTolerance) ? movementSpeed : 0);
        }
        else if (AIType == Type.Hide)
        {
            if (distance < targetPositionTolerance)
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Crouch"))
                    anim.CrossFade("Crouch", 0.5f);
        }
        else if (AIType == Type.Dance)
        {
            if (distance < targetPositionTolerance)
                anim.CrossFade("dance " + danceIndex, 0f);
        }
        else if (AIType == Type.Drive)
        {
            if (distance > targetPositionTolerance)
                anim.SetFloat("Forward", movementSpeed);
            else
                anim.CrossFade("Crouch", 1f);
        }
    }
}
