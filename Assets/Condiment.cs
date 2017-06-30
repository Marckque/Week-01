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

    private bool hasCooldown = true;

    private bool saltUsed;
    private bool pepperUsed;

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

            if (Input.GetKeyUp(KeyCode.S))
            {
                StartCoroutine(StartCooldown());
            }
        }
        else
        {
            moveToTarget = Input.GetKey(KeyCode.P);

            if (Input.GetKeyUp(KeyCode.P))
            {
                StartCoroutine(StartCooldown());
            }
        }

        if (otherCondiment.moveToTarget || !hasCooldown) moveToTarget = false;

        if (moveToTarget)
        {
            {
                condimentParticle.Play();
            }

            Vector3 vel = Vector3.zero;
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref vel, 0.1f);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.down), Time.deltaTime * rotationSpeed);
        }
        else
        {
            if (condimentParticle.isPlaying)
            {
                condimentParticle.Stop();
            }

            Vector3 vel = Vector3.zero;
            transform.position = Vector3.SmoothDamp(transform.position, originalPosition, ref vel, 0.1f);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.up), Time.deltaTime * rotationSpeed);
        }
    }

    private IEnumerator StartCooldown()
    {
        hasCooldown = false;
        yield return new WaitForSeconds(.25f);
        hasCooldown = true;
    }
}