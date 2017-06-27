using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gamba : Ingredient
{
    public Transform target;
    public float velocityMagnitude;
    public float maximumVelocity;

    public float delayBeforeDeactivation = 2f;
    private float delayElapsed;

    private List<Pan> connectedPan = new List<Pan>();
    private bool isAttractedToPan;
    private Rigidbody entityRigidbody;
    private MeshRenderer meshRenderer;

    private float randomCookingOffset;

	protected void Start()
    {
        delayElapsed = delayBeforeDeactivation;
        entityRigidbody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        randomCookingOffset = Random.Range(0.9f, 1.1f);
	}

    protected void Update()
    {
        if (connectedPan.Count > 0)
        {
            cookingValue += ((connectedPan[0].heatingPower * Time.deltaTime * 0.01f) * randomCookingOffset);
            Cooking();
        }
    }

    protected void FixedUpdate()
    {
        if (isAttractedToPan)
        {
            Vector3 direction = target.position - transform.position;
            entityRigidbody.AddForce(direction * velocityMagnitude);

            entityRigidbody.velocity = Vector3.ClampMagnitude(entityRigidbody.velocity, maximumVelocity);
        }
	}

    protected void OnTriggerEnter(Collider other)
    {
        Pan pan = other.GetComponent<Pan>();

        if (pan)
        {
            if (!connectedPan.Contains(pan))
            {
                connectedPan.Add(pan);
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        Pan pan = other.GetComponent<Pan>();

        if (pan)
        {
            if (connectedPan.Contains(pan))
            {
                connectedPan.Remove(pan);
            }
        }
    }

    private void Cooking()
    {
        meshRenderer.material.SetFloat("_CookedPercentage", cookingValue);
    }
    
    protected void OnDrawGizmosSelected()
    {
        Vector3 direction = target.position - transform.position;

        Ray ray = new Ray(transform.position, direction);
        Gizmos.DrawRay(ray);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(ray.direction, .2f);
    }
}