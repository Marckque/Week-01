using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColliders : MonoBehaviour
{
    [Range(0f, .1f)]
    public float amplitude = 1f;
    [Range(0f, 1f)]
    public float offset;
    private Transform[] children;

    protected void Start()
    {
        Initialise();
        SetAngles();
    }

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

        for (int i = 0; i < children.Length; i++)
        {
            float x = amplitude * Mathf.Cos(angle) * Mathf.Rad2Deg;
            float z = amplitude * Mathf.Sin(angle) * Mathf.Rad2Deg;

            children[i].transform.position = new Vector3(x, children[i].transform.position.y, z);

            children[i].LookAt(transform);

            angle += offset;
        }
    }
}