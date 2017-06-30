using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColliders : MonoBehaviour
{
    [Range(0f, 10f)]
    public float amplitude = 1f;
    private Transform[] children;

    protected void OnValidate()
    {
        Initialise();
        SetAngles();
    }

    protected void Initialise()
    {
        children = new Transform[transform.childCount];

        for (int i = 0; i < children.Length; i++)
        {
            children[i] = transform.GetChild(i);
        }
    }

    protected void SetAngles()
    {
        float angle = 360f / children.Length;
        float currentAngle = 0f;

        for (int i = 0; i < children.Length; i++)
        {
            float x = amplitude * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
            float z = amplitude * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

            children[i].transform.position = transform.position + new Vector3(x, transform.localPosition.y, z);

            children[i].LookAt(transform);
            currentAngle += angle;
        }
    }
}