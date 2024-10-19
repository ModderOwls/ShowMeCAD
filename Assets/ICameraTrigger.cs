using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraTrigger
{
    /// <summary>
    /// Set camera position to this upon interacting with it.
    /// </summary>
    protected Vector3 newPosition { get; set; }

    /// <summary>
    /// Set camera rotation to this (in euler) upon interacting with it.
    /// </summary>
    protected Vector3 newRotation { get; set; }
}
