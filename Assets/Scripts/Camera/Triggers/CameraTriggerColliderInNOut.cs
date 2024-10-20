using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerColliderInNOut : CameraTriggerCollider
{
    [Header("Collider In n Out")]

    [Tooltip("Update the previous trigger it'll go back to. Note that this might mess with this trigger itself.")]
    public bool updateLastTrigger;

    ICameraTrigger lastTrigger;


    public override void Set(CameraController controller)
    {
        lastTrigger = controller.trigger;

        base.Set(controller);
    }

    public override void React()
    {
        if (updateLastTrigger)
        {
            lastTrigger.React();
        }

        base.React();
    }

    public void OnTriggerExit(Collider other)
    {
        controller.SetTrigger(lastTrigger);
    }
}
