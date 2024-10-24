using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSightIcon : MonoBehaviour
{
    [Header("Values")]

    public float barValue;


    [Header("Materials")]

    public Material matLook;
    public Material matFound;


    [Header("References")]

    public Transform bar;
    MeshRenderer meshRender;
    Camera cam;


    void Start()
    {
        cam = Camera.main;

        meshRender = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (barValue == 0)
        {
            meshRender.enabled = false;
            bar.gameObject.SetActive(false);

            return;
        }

        meshRender.enabled = true;
        bar.gameObject.SetActive(true);

        transform.LookAt(cam.transform);
        transform.eulerAngles += Vector3.up * 180;

        bar.localScale = new Vector3(1.2f, barValue * 1.2f, 1.2f);

        if (barValue == 1)
        {
            meshRender.material = matFound;

            return;
        }

        meshRender.material = matLook;
    }
}
