using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraBehaviour
{
    public Transform cameraObjData { get; set; }

    public void React() { }
}
