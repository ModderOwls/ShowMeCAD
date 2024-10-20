using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerCollider : CameraTrigger
{
    public override void Set(CameraController controller)
    {
        base.Set(controller);

        controller.transform.position = cameraObjData.position;
        controller.transform.rotation = cameraObjData.rotation;
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (!controller) controller = Camera.main.GetComponent<CameraController>();

        controller.SetTrigger(this);
    }
}
