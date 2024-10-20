using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraTrigger
{
    /// <summary>
    /// Set controller with reference to controller.
    /// </summary>
    public void Set(CameraController controller) { }

    /// <summary>
    /// Update the current trigger on every frame if its the active trigger.
    /// </summary>
    public void React() { }

    /// <summary>
    /// Done before calling Set on the next trigger.
    /// </summary>
    public void End() { }
}
