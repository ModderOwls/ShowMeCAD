using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerCollider : MonoBehaviour, ICameraTrigger
{
    public Transform cameraObjData;

    Transform cam;

    Vector3 ICameraTrigger.newPosition { get; set; }
    Vector3 ICameraTrigger.newRotation { get; set; }
    
    public void OnTriggerEnter(Collider collider)
    {
        if (!cam) cam = Camera.main.transform;

        cam.transform.position = cameraObjData.position;
        cam.transform.rotation = cameraObjData.rotation;
    }

    public void OnTriggerExit(Collider collider)
    {

    }

    void OnDrawGizmosSelected()
    {
        if (cameraObjData != null)
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawLine(cameraObjData.position, cameraObjData.position + cameraObjData.forward * .5f);
            Gizmos.DrawLine(cameraObjData.position + cameraObjData.forward * .5f, cameraObjData.position + cameraObjData.forward * .25f + cameraObjData.right * .1f);
            Gizmos.DrawLine(cameraObjData.position + cameraObjData.forward * .5f, cameraObjData.position + cameraObjData.forward * .25f - cameraObjData.right * .1f);
        }
    }
}
