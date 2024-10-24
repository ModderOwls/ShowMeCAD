using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSightCone : MonoBehaviour
{
    [Header("Sight")]

    public float radius = 7.5f;
    float trueRadius;
    public float angle = 45;

    public float suspiciounSpeed = .5f;
    public float suspiciounLoss = .5f;

    [Header("Values")]

    public float suspicion = 1;
    bool inSight;


    [Header("References")]

    public Transform target;
    public NPCInteractor interactor;
    public NPCSightIcon icon;

    void OnValidate()
    {
        trueRadius = radius * radius;
    }

    void Start()
    {
        trueRadius = radius * radius;
    }

    public void FixedUpdate()
    {
        icon.barValue = (suspicion - 1) / 9;

        if (inSight || suspicion == 10) return;

        suspicion -= .5f * Time.fixedDeltaTime;

        if (suspicion < 1) suspicion = 1;
    }

    public void DetectTarget()
    {
        float targetAngle = Mathf.Abs(Vector3.Angle((target.position + Vector3.up * .5f) - transform.position, transform.forward));

        inSight = false;

        if (targetAngle * 2 > angle) return;

        inSight = true;

        if (suspicion >= 10)
        {
            suspicion = 10;

            interactor.OnFound();

            return;
        }

        float distanceAmplifier = radius - (transform.position - target.position).magnitude;

        if (distanceAmplifier < 0) distanceAmplifier = 0;

        Debug.Log(targetAngle);

        suspicion *= 1 + suspiciounSpeed * Time.fixedDeltaTime * distanceAmplifier * Mathf.Sin((angle - targetAngle * 2) * 2 * Mathf.Deg2Rad);

        interactor.OnSight();
    }

    void OnTriggerEnter(Collider other)
    {
        target = other.transform;

        interactor = other.GetComponent<NPCInteractor>();
        

        if (!interactor) return;

        interactor.OnSpotted();
    }

    void OnTriggerStay(Collider other)
    {
        if (!interactor) return;

        DetectTarget();
    }

    void OnTriggerExit(Collider other)
    {
        target = null;
        interactor = null;

        inSight = false;

        if (!interactor) return;

        interactor.OnSpotted();
    }
}
