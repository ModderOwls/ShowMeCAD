using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour, ICameraBehaviour
{
    [Header("Camera Trigger")]

    public Transform cameraObjData;

    public Color gizmoColor = Color.red;


    public virtual void Set(CameraController controller) { }

    public virtual void React() { }

    public virtual void End() { }

    public void UpdateCamera(Transform camera)
    {
        camera.transform.position = cameraObjData.position;
        camera.transform.rotation = cameraObjData.rotation;
    }
}
