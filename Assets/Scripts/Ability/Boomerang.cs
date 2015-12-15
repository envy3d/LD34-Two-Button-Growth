﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Boomerang : MonoBehaviour
{
    public float launchForce = 50;
    public float returnForce = 10;
    public float maxVelocity = 30;
    public float lifetime = 15;

    public float targetSpinOutAmout = 270;
    public float targetSpinTime = 1;
    public AnimationCurve targetSpinCurve;
    public float targetKillEngineTime = 0.8f;

    private Rigidbody rb;
    private GameObject launchedFromGO;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.up * launchForce, ForceMode.Impulse);

        DestroyObject(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.down * returnForce, ForceMode.Force);
        Vector3.ClampMagnitude(rb.velocity, maxVelocity);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("vehicle"))
        {
            VehicleController vc = other.GetComponent<VehicleController>();
            if (vc != null && vc.gameObject != launchedFromGO)
            {
                vc.SpinOut(targetSpinOutAmout, targetSpinTime, targetSpinCurve);
                vc.KillEngine(targetKillEngineTime);
                Destroy(gameObject);
            }
        }
    }

    public void Launch(GameObject launchedFrom)
    {
        launchedFromGO = launchedFrom;
        StartCoroutine(RemoveLaunchProtection());
    }

    private IEnumerator RemoveLaunchProtection()
    {
        yield return new WaitForSeconds(0.5f);
        launchedFromGO = null;
    }
}
