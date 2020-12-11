using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLightController : MonoBehaviour
{
    //Duration of each status 
    public float _redDuration = 5, _yellowDuration = 1, _greenDuration = 5;

    //Renderer to set color according to the status
    private Renderer _renderer;

    //Definition of the status
    public enum StreetlightStatus { Blank, Green, Yellow, Red };
    public StreetlightStatus _currStatus = StreetlightStatus.Yellow;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        StartCoroutine("ChangeStatus");
    }

    private IEnumerator ChangeStatus()
    {
        while (true)
        {
            float waitTime = 0.0f;
            StreetlightStatus nextStatus = StreetlightStatus.Blank;

            switch (_currStatus)
            {
                case StreetlightStatus.Green:
                    waitTime = _greenDuration; //Set wait time 
                    nextStatus = StreetlightStatus.Yellow; //Set next status
                    _renderer.material.SetColor("_Color", Color.green);
                    break;
                case StreetlightStatus.Red:
                    waitTime = _redDuration;
                    nextStatus = StreetlightStatus.Green;
                    _renderer.material.SetColor("_Color", Color.red);
                    break;
                case StreetlightStatus.Yellow:
                    waitTime = _yellowDuration;
                    nextStatus = StreetlightStatus.Red;
                    _renderer.material.SetColor("_Color", Color.yellow);
                    break;
            }

            yield return new WaitForSeconds(waitTime);

            //Change Status after X seconds
            _currStatus = nextStatus;
        }
    }

    public bool CanWalk()
    {
        return _currStatus == StreetlightStatus.Green;
    }
}
