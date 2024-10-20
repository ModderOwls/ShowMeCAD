using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour, ICameraTrigger
{
    [Header("Camera Trigger")]

    public Transform cameraObjData;

    public Color gizmoColor = Color.red;


    protected CameraController controller;

    protected ICameraBehaviour behaviour;


    public virtual void Set(CameraController controller)
    {
        this.controller = controller;

        if (behaviour == null)
        {
            behaviour = GetComponent<ICameraBehaviour>();

            //If it couldn't find an attached CameraBehaviour.
            if (behaviour == null)
            {
                Debug.LogError("Couldn't find a CameraBehaviour on object '" + gameObject.name + "'. Resorting to instant behaviour, please add one to the object.");

                behaviour = gameObject.AddComponent<CameraBehaviourInstant>();
            }
        }
    }

    public virtual void React()
    {
        behaviour.React();
    }

    public virtual void End() { }


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
