using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;

    public Vector3 inputDirection;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void SetEuler(Vector3 euler)
    {
        transform.eulerAngles = euler;
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
