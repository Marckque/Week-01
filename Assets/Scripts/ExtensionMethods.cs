using UnityEngine;
using System.Collections;

public static class ExtensionMethods
{
    public static float Remap(this float a_Value, float a_From1, float a_To1, float a_From2, float a_To2)
    {
        return (a_Value - a_From1) / (a_To1 - a_From1) * (a_To2 - a_From2) + a_From2;
    }

    public static Vector3 RandomUnifiedVector3(float a_Scalar = 1f)
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * a_Scalar;
    }

    public static Vector3 RandomVector3(float a_ScalarX = 1f, float a_ScalarY = 1f, float a_ScalarZ = 1f)
    {
        return new Vector3(Random.Range(-1f, 1f) * a_ScalarX, Random.Range(-1f, 1f) * a_ScalarY, Random.Range(-1f, 1f) * a_ScalarZ);
    }
}