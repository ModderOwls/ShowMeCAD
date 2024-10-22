using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSightCone : MonoBehaviour
{
    public float angle = 45;

    public Transform target;

    public void DetectTarget()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        target = other.transform;


    }

    void OnTriggerStay(Collider other)
    {
        DetectTarget();
    }

    void OnTriggerExit(Collider other)
    {
        if (target == other)
        {
            target = null;
        }
    }
}
