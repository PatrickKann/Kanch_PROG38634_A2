using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDoorController : MonoBehaviour
{
    //Duration of each status 
    public float _closeDuration = 5, _openDuration = 5;

    //Height of each status 
    private float _closeHeight, _openHeight, _currHeight;
    private float _movingRate = 0.2f;

    //Renderer
    private Renderer _renderer;

    //Definition of the status
    public enum DoorStatus { Blank, Close, Open };
    public DoorStatus _currStatus = DoorStatus.Close;

    void Start()
    {
        _renderer = this.GetComponent<Renderer>();
        StartCoroutine("ChangeStatus");
        _closeHeight = _currHeight = this.transform.position.y;
        _openHeight = _closeHeight + 1.25f;
    }

    private IEnumerator ChangeStatus()
    {
        while (true)
        {
            float waitTime = 0.0f;
            DoorStatus nextStatus = DoorStatus.Blank;

            switch (_currStatus)
            {
                case DoorStatus.Close:
                    waitTime = _closeDuration; //Set wait time 
                    nextStatus = DoorStatus.Open; //Set next status
                    _renderer.material.SetColor("_Color", Color.red);
                    break;
                case DoorStatus.Open:
                    waitTime = _openDuration;
                    nextStatus = DoorStatus.Close;
                    _renderer.material.SetColor("_Color", Color.green);
                    this.gameObject.GetComponent<DoorController>().Open();
                    break;
            }

            yield return new WaitForSeconds(waitTime);

            //Change Status after X seconds
            _currStatus = nextStatus;
        }
    }
}
