using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour, ICameraTrigger
{
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

        behaviour.Set(controller);
    }

    public virtual void React()
    {
        behaviour.React();
    }

    public virtual void End()
    {
        behaviour.End();
    }
}
