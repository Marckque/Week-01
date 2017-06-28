using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    public float smooth;
    private Vector3 velocity = Vector3.zero;

    protected void Start()
    {
        Cursor.visible = false;
    }

    protected void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, Input.mousePosition, ref velocity, smooth);
    }
}