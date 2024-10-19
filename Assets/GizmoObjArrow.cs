using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoObjArrow : MonoBehaviour
{
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(transform.position, transform.position + transform.forward * .5f);
        Gizmos.DrawLine(transform.position + transform.forward * .5f, transform.position + transform.forward * .25f + transform.right * .1f);
        Gizmos.DrawLine(transform.position + transform.forward * .5f, transform.position + transform.forward * .25f - transform.right * .1f);
    }
}
