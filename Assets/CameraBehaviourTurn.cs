using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviourTurn : CameraBehaviour
{
    [Header("Settings")]

    public float turnSpeed;


    [Header("References")]

    Transform player;
    CameraController controller;


    public override void Set(CameraController controller)
    {
        this.controller = controller;

        player = controller.player;
    }

    public override void React()
    {
        if (!player)
        {
            Debug.LogError("No player was set on '" + gameObject.name +  "'. Something is wrong.");

            return;
        }

        Vector3 rotationDirection = (player.position - cameraObjData.position).normalized;
        Quaternion lookDirection = Quaternion.LookRotation(rotationDirection);
        cameraObjData.rotation = Quaternion.Slerp(cameraObjData.rotation, lookDirection, Time.deltaTime * turnSpeed);

        UpdateCamera(controller.transform);
    }


    void OnDrawGizmos()
    {
        if (cameraObjData != null)
        {
            Gizmos.color = gizmoColor;

            Gizmos.DrawSphere(cameraObjData.position, .2f);
            Gizmos.DrawLine(cameraObjData.position + cameraObjData.forward * .5f, cameraObjData.position + cameraObjData.forward * .25f + cameraObjData.right * .1f);
            Gizmos.DrawLine(cameraObjData.position + cameraObjData.forward * .5f, cameraObjData.position + cameraObjData.forward * .25f - cameraObjData.right * .1f);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (cameraObjData != null)
        {
            Gizmos.color = new Color(1, 0.4f, 0);

            Gizmos.DrawSphere(cameraObjData.position, .2f);
            Gizmos.DrawLine(cameraObjData.position + cameraObjData.forward * .5f, cameraObjData.position + cameraObjData.forward * .25f + cameraObjData.right * .1f);
            Gizmos.DrawLine(cameraObjData.position + cameraObjData.forward * .5f, cameraObjData.position + cameraObjData.forward * .25f - cameraObjData.right * .1f);
        }
    }
}
