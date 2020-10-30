using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    public GameObject prop;
    public Transform targetBone;
    public Vector3 positionOffset;
    public Vector3 rotationOffset;
    public bool destroyTrigger = true;

    public GameObject player;

    void OnTriggerEnter(Collider collision)
    {
        bool addPropCondition = targetBone.IsChildOf(collision.transform) & !AlreadyHalreadyHasChildObject();
        if (addPropCondition)
            AddProp();
    }

    private void AddProp()
    {
        GameObject newprop;
        newprop = Instantiate(prop, targetBone.position,
                  targetBone.rotation) as GameObject;
        newprop.name = prop.name;
        newprop.transform.parent = targetBone;
        newprop.transform.localPosition = positionOffset;
        newprop.transform.localEulerAngles =  rotationOffset;
        if (destroyTrigger)
            Destroy(gameObject);

        if (player)
        {
            player.GetComponent<YBotMovement>().isHoldingGun = true;
            player.GetComponent<MouseAimLookAt>().weapon = newprop.transform;
        }
    }

    private bool AlreadyHalreadyHasChildObject()
    {
        string propName = prop.name;
        foreach (Transform child in targetBone)
        {
            if (child.name == propName)
                return true;
        }
        return false;
    }
}
