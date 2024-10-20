using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerCollider : CameraTrigger
{    
    public void OnTriggerEnter(Collider other)
    {
        if (!controller) controller = Camera.main.GetComponent<CameraController>();

        controller.SetTrigger(this);
    }
}
