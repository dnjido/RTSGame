using UnityEngine;

public class HealthCalc
{
    public static float Change(float d, float current, float armor)
    {
        d -= Mathf.Round(armor / 100 * d);
        return Mathf.Clamp(current + d, 0, float.MaxValue);
    }

    public static float GetHealth(float current) => current;

    public static float GetPercent(float current, float max) => current / max;
}

