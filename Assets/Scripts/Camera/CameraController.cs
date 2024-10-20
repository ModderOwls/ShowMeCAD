using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Player")]

    public Transform player;
    public Vector3 inputDirection;


    [Header("Triggers")]

    public ICameraTrigger trigger;


    [Header("References")]

    Camera cam;


    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (trigger != null)
        {
            trigger.React();
        }
    }

    public void SetTrigger(ICameraTrigger trigger)
    {
        if(this.trigger != null) this.trigger.End();

        if (trigger != null) trigger.Set(this);

        this.trigger = trigger;
    }

    public Vector3 ResetInputDirection(Vector2 input)
    {
        Vector3 relativeRight = transform.right;
        Vector3 relativeForward = transform.forward;

        relativeRight.y = 0;
        relativeForward.y = 0;

        relativeRight = relativeRight.normalized * input.x;
        relativeForward = relativeForward.normalized * input.y;

        inputDirection = relativeRight + relativeForward;

        return inputDirection;
    }
}
