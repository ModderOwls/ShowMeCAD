using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviourSplineLookAt : MonoBehaviour
{
    public CameraBehaviourSpline behaviour;

    public Transform follow;

    public Vector3 offset;

    void Update()
    {
        if (follow != null)
        {
            if (!behaviour.objFollow) return;

            Vector3 basePoint = behaviour.cameraObjData[0].position + behaviour.objFollowOffset;
            transform.position = behaviour.Amplify(behaviour.objFollow.position - basePoint) + basePoint + behaviour.objFollowOffset + offset;
        }
    }
}
