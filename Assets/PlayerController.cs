using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]

    public float acceleration;
    public float maxSpeed;
    public float drag;
    public float linearDrag;


    [Header("Input")]

    public Vector2 inputMove;


    [Header("Values")]

    public Vector3 velocity;
    public Vector3 moveDirection;
    public Vector3 groundNormal;


    [Header("References")]

    CharacterController control;
    CameraController cam;

    LayerMask layersGround;


    [Header("Collision")]

    RaycastHit rayGround;


    void Awake()
    {
        control = GetComponent<CharacterController>();
        cam = Camera.main.GetComponent<CameraController>();

        layersGround = LayerMask.GetMask("Ground");
    }

    void FixedUpdate()
    {
        Move();

        DetectRamp();
    }

    void Move()
    {
        Vector2 input = new Vector2(moveDirection.x, moveDirection.z) * acceleration;
        Vector2 vel2D = new Vector2(velocity.x, velocity.z);

        vel2D *= drag;

        vel2D += input;

        Vector2 velNorm = vel2D.normalized;

        if (vel2D.sqrMagnitude > maxSpeed * maxSpeed)
        {
            vel2D = velNorm * maxSpeed;
        }

        if (vel2D.sqrMagnitude > linearDrag * linearDrag)
        {
            vel2D -= velNorm * linearDrag;
        }
        else
        {
            vel2D = Vector2.zero;
        }

        velocity = new Vector3(vel2D.x, velocity.y, vel2D.y);
        control.Move(velocity * Time.fixedDeltaTime);
    }

    void DetectRamp()
    {
        Ray groundRay = new Ray(transform.position, -transform.up * (control.height / 2 + .3f));
        Debug.DrawRay(groundRay.origin, groundRay.direction, Color.blue);

        if (Physics.Raycast(groundRay, out rayGround, 100, layersGround))
        {
            groundNormal = rayGround.normal;
            transform.position = rayGround.point + transform.up * (control.height / 2);
        }
    }

    public void InputMove(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();

        moveDirection = cam.ResetInputDirection(inputMove);
    }
}
