
using UnityEngine;
using System;

public struct DistanceAndSplineT
{
    public float distance;
    public float t;

    public DistanceAndSplineT(float distance, float t)
    {
        this.distance = distance;
        this.t = t;
    }
}
