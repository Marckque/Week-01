using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gamba : Ingredient
{
    public float velocityMagnitude;
    public float maximumVelocity;

    public float delayBeforeDeactivation = 2f;
    private float delayElapsed;

    private bool isAttractedToPan = true;
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
        if (connectedCookingDevice.Count > 0)
        {
            cookingValue += ((connectedCookingDevice[0].heatingPower * Time.deltaTime * 0.01f) * randomCookingOffset);
            UpdateVisualsAkaCooking();
        }
    }

    protected void FixedUpdate()
    {
        if (isAttractedToPan)
        {
            Vector3 direction = cookingDeviceRoot.position - transform.position;
            entityRigidbody.AddForce(direction * velocityMagnitude);

            entityRigidbody.velocity = Vector3.ClampMagnitude(entityRigidbody.velocity, maximumVelocity);
        }
	}

    private void UpdateVisualsAkaCooking()
    {
        meshRenderer.material.SetFloat("_CookedPercentage", cookingValue);
    }
    
    protected void OnDrawGizmosSelected()
    {
        Vector3 direction = cookingDeviceRoot.position - transform.position;

        Ray ray = new Ray(transform.position, direction);
        Gizmos.DrawRay(ray);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(ray.direction, .2f);
    }
}