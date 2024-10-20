using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviourTurn : MonoBehaviour
{
    [Header("Settings")]

    public float interval;
    public float speed;
    public float degrees;


    [Header("Values")]

    Quaternion startRotation;
    Quaternion endRotation;
    float timePassed;
    bool rotated;

    void Start()
    {
        startRotation = transform.rotation;
        endRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + degrees, transform.eulerAngles.z);
    }

    //Could (and should) use fixedUpdate, but its a prototype so it looks better and I likely wont re-use, so eh.
    void Update()
    {
        timePassed += Time.deltaTime;

        transform.rotation = Quaternion.Lerp(transform.rotation, endRotation, timePassed * speed);

        while (timePassed > interval)
        {
            Quaternion oldRotation = startRotation;
            startRotation = endRotation;
            endRotation = oldRotation;

            timePassed -= interval;
        }
    }
}
