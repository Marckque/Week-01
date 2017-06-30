using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condiment : MonoBehaviour
{
    public Condiment otherCondiment;
    public bool isSalt;
    public Transform target;
    public ParticleSystem condimentParticle;

    private Vector3 originalPosition;
    private Vector3 originalEulerAngles;

    private bool moveToTarget;

    public float rotationSpeed;

    private void Start()
    {
        originalPosition = transform.position;
        originalEulerAngles = transform.eulerAngles;
    }

    private void Update()
    {
        if (isSalt)
        {
            moveToTarget = Input.GetKey(KeyCode.S);
        }
        else
        {
            moveToTarget = Input.GetKey(KeyCode.P);
        }

        if (otherCondiment.moveToTarget) moveToTarget = false;

        if (moveToTarget)
        {
            condimentParticle.Emit(1);

            Vector3 vel = Vector3.zero;
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref vel, 0.1f);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.down), Time.deltaTime * rotationSpeed);
        }
        else
        {
            Vector3 vel = Vector3.zero;
            transform.position = Vector3.SmoothDamp(transform.position, originalPosition, ref vel, 0.1f);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.up), Time.deltaTime * rotationSpeed);
        }
    }
}