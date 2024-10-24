using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviourSmooth : CameraBehaviour
{
    [Header("Smooth")]

    public float smoothSpeed;
    
    float distance;
    Quaternion startRotation;
    bool busy;

    [Header("References")]

    CameraController controller;


    public override void Set(CameraController controller)
    {
        this.controller = controller;

        distance = (transform.position - controller.transform.position).magnitude;
        startRotation = controller.transform.rotation;
        busy = true;
    }

    public override void React()
    {
        if (!busy) return;

        float newDistance = (cameraObjData.position - controller.transform.position).magnitude;
        if (newDistance < 0.03f)
        {
            busy = false;

            controller.transform.position = cameraObjData.position;
            controller.transform.rotation = cameraObjData.rotation;

            return;
        }

        controller.transform.position = Vector3.Lerp(controller.transform.position, cameraObjData.position, Time.deltaTime * smoothSpeed);
        controller.transform.rotation = Quaternion.Lerp(startRotation, cameraObjData.rotation, 1 - newDistance / distance);
    }

    public override void End()
    {
        busy = false;
    }


    void OnDrawGizmos()
    {
        if (cameraObjData != null)
        {
            Gizmos.color = gizmoColor;

            Gizmos.DrawLine(cameraObjData.position, cameraObjData.position + cameraObjData.forward * .5f);
            Gizmos.DrawLine(cameraObjData.position + cameraObjData.forward * .5f, cameraObjData.position + cameraObjData.forward * .25f + cameraObjData.right * .1f);
            Gizmos.DrawLine(cameraObjData.position + cameraObjData.forward * .5f, cameraObjData.position + cameraObjData.forward * .25f - cameraObjData.right * .1f);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (cameraObjData != null)
        {
            Gizmos.color = new Color(1, 0.4f, 0);

            Gizmos.DrawLine(cameraObjData.position, cameraObjData.position + cameraObjData.forward * .5f);
            Gizmos.DrawLine(cameraObjData.position + cameraObjData.forward * .5f, cameraObjData.position + cameraObjData.forward * .25f + cameraObjData.right * .1f);
            Gizmos.DrawLine(cameraObjData.position + cameraObjData.forward * .5f, cameraObjData.position + cameraObjData.forward * .25f - cameraObjData.right * .1f);
        }
    }
}
