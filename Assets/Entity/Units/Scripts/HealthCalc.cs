using System;
using UnityEngine;

public class HealthCalc // Calculate health with armor reduce
{
    public static float Change(float d, float current, float armor)
    {
        d -= Mathf.Round(armor / 100 * d);
        return Mathf.Clamp(current + d, 0, float.MaxValue);
    }
}

