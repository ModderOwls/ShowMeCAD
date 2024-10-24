using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Doesn't use CameraBehaviour, but ICameraBehaviour as it functions too differently.
public class CameraBehaviourSpline : MonoBehaviour, ICameraBehaviour
{
    [Header("Camera Behaviour")]

    public Transform[] cameraObjData;

    public Color gizmoColor = Color.blue;


    [Header("Spline")]

    public Transform objFollow;
    Vector3 objFollowPosition;

    [Tooltip("Amplifies the objFollow position for more precise control.")]
    public Vector3 objFollowAmplifier = Vector3.one;
    public Vector3 objFollowOffset;

    public Transform objLookAt;


    [Header("References")]

    CameraController controller;


    //Still keep everything virtual for if other scripts want to inherit this.
    public virtual void Set(CameraController controller)
    {
        this.controller = controller;

        if (!objFollow) objFollow = controller.player;
        if (!objLookAt) objLookAt = controller.player;

        if (cameraObjData.Length < 2)
        {
            Debug.LogError("You didnt apply 2 or more points for the spline '" + gameObject.name + "'.");
            Debug.Break();
        }
    }

    public virtual void React()
    {
        objFollowPosition = Amplify(objFollow.position);

        UpdateCamera(controller.transform, GetNearestPoint());
    }

    public virtual void End() { }

    //Should prolly cache the current point and just check if its near to one of two closest points, but not worth it for a prototype.
    public Vector3 GetNearestPoint()
    {
        int nearestPoint = 0;
        float nearestDistanceSqr = (objFollowPosition - DataGetAmplify(0)).sqrMagnitude;

        for (int i = 1; i < cameraObjData.Length; i++)
        {
            float distanceSqr = (objFollowPosition - DataGetAmplify(i)).sqrMagnitude;
            if (nearestDistanceSqr >= distanceSqr)
            {
                nearestPoint = i;
                nearestDistanceSqr = distanceSqr;
            }
        }

        if (nearestPoint == 0) return Interpolate(cameraObjData[0].position, cameraObjData[1].position);

        if (nearestPoint == cameraObjData.Length - 1) return Interpolate(cameraObjData[^1].position, cameraObjData[^2].position);

        int nearestSecondPoint = nearestPoint - 1;
        float nearestSecondDistanceSqr = (objFollowPosition - DataGetAmplify(nearestSecondPoint)).sqrMagnitude;

        if (nearestSecondDistanceSqr > (objFollowPosition - DataGetAmplify(nearestPoint + 1)).sqrMagnitude)
        {
            nearestSecondPoint = nearestPoint + 1;
        }

        return Interpolate(cameraObjData[nearestPoint].position, cameraObjData[nearestSecondPoint].position);
    }

    Vector3 Interpolate(Vector3 pointOne, Vector3 pointTwo)
    {
        Vector3 line = Amplify(pointTwo - pointOne);
        float lineDistance = line.magnitude;
        Vector3 lineNorm = line.normalized;

        Vector3 lineFollow = objFollowPosition - (Amplify(pointOne) + objFollowOffset);
        float dot = Vector3.Dot(lineFollow, lineNorm) / lineDistance;

        if (dot < 0)
        {
            dot = 0;
        }

        float lineDistanceUnamp = (pointTwo - pointOne).magnitude;
        Vector3 lineNormUnamp = (pointTwo - pointOne).normalized;

        return pointOne + lineNormUnamp * (dot * lineDistanceUnamp);
    }

    public void UpdateCamera(Transform camera, Vector3 point)
    {
        camera.position = point;
        camera.LookAt(objLookAt);
    }


    public Vector3 Amplify(Vector3 vector)
    {
        return new Vector3(vector.x * objFollowAmplifier.x, vector.y * objFollowAmplifier.y, vector.z * objFollowAmplifier.z);
    }
    public Vector3 DataGetAmplify(int indexData)
    {
        return Amplify(cameraObjData[indexData].position) + objFollowOffset;
    }


    void OnDrawGizmos()
    {
        if (cameraObjData != null)
        {
            Gizmos.color = gizmoColor;

            Vector3 lastPoint = cameraObjData[0].position;
            for (int i = 0; i < cameraObjData.Length; ++i)
            {
                Vector3 pos = cameraObjData[i].position;

                Gizmos.DrawLine(lastPoint, pos);

                Gizmos.DrawLine(pos + Vector3.down * .1f, pos + Vector3.up * .1f);
                Gizmos.DrawLine(pos + Vector3.left * .1f, pos + Vector3.right * .1f);
                Gizmos.DrawLine(pos + Vector3.back * .1f, pos + Vector3.forward * .1f);

                lastPoint = pos;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (cameraObjData != null)
        {
            Gizmos.color = new Color(1, 0.4f, 0);

            Vector3 basePoint = CheckForZeroAmplifier(objFollowOffset);

            Vector3 lastPoint = Amplify(cameraObjData[0].position) + basePoint;
            for (int i = 0; i < cameraObjData.Length; ++i)
            {
                Vector3 pos = Amplify(cameraObjData[i].position) + basePoint;

                Gizmos.DrawLine(lastPoint, pos);

                Gizmos.DrawLine(pos + Vector3.down * .1f, pos + Vector3.up * .1f);
                Gizmos.DrawLine(pos + Vector3.left * .1f, pos + Vector3.right * .1f);
                Gizmos.DrawLine(pos + Vector3.back * .1f, pos + Vector3.forward * .1f);

                lastPoint = pos;
            }

            RaycastHit screenRay;
            if (Physics.Raycast(HandleUtility.GUIPointToWorldRay(Event.current.mousePosition), out screenRay, 100, LayerMask.GetMask("Ground")))
            {
                objFollowPosition = Amplify(screenRay.point);

                Gizmos.DrawSphere(screenRay.point, .25f);
                Gizmos.DrawSphere(Amplify(screenRay.point - basePoint) + basePoint, 0.2f);
                Gizmos.DrawCube(GetNearestPoint(), Vector3.one * .25f);
            }
        }
    }

    //Only used in debugging/gizmos as it's stinky and slow.
    private Vector3 CheckForZeroAmplifier(Vector3 vector)
    {
        if (objFollowAmplifier.x == 0) vector.x += transform.position.x;
        if (objFollowAmplifier.y == 0) vector.y += transform.position.y;
        if (objFollowAmplifier.z == 0) vector.z += transform.position.z;

        return vector;
    }
}
