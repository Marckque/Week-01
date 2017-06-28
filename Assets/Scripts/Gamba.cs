using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gamba : Ingredient
{
    [Header("Gamba")]
    [Header("Velocity")]
    public float velocityMagnitude;
    public float maximumVelocity;

    public float delayBeforeDeactivation = 1f;
    private float delayElapsed;

    private bool isAttractedToPan = true;
    private MeshRenderer meshRenderer;

    private float timeWithoutConnectedDevice;
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

        if (ConnectedCookingDevice.Count <= 0)
        {
            timeWithoutConnectedDevice += Time.deltaTime;

            if (timeWithoutConnectedDevice > 1f)
            {
                isAttractedToPan = false;
            }
        }
        else
        {
            timeWithoutConnectedDevice = 0f;
            isAttractedToPan = true;
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
}