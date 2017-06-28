using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ingredient : MonoBehaviour
{
    public ParticleSystem destroyedVFX;
    protected float cookingValue;
    protected List<CookingDevice> connectedCookingDevice = new List<CookingDevice>();
    public List<CookingDevice> ConnectedCookingDevice { get { return connectedCookingDevice; } }
    public Transform cookingDeviceRoot { get; set; }
    public Transform ingredientSpawnerRoot { get; set; }
    private Vector3 endPosition;
    protected Rigidbody entityRigidbody;

    private void Start()
    {
        entityRigidbody = GetComponent<Rigidbody>();
        endPosition = new Vector3(0f, 3f, -15f);
    }

    public void ChangeToNewParent(bool value)
    {
        if (value)
        {
            transform.SetParent(cookingDeviceRoot);
        }
        else
        {
            transform.SetParent(ingredientSpawnerRoot);
        }
    }
    
    public void SetIsKinematic(bool value)
    {
        if (ConnectedCookingDevice.Count > 0)
        {
            entityRigidbody.isKinematic = value;
        }
    }

    public void SetUseGravity(bool value)
    {
        entityRigidbody.isKinematic = value;
    }

    public void EatIngredient()
    {
        ParticleSystem ps = Instantiate(destroyedVFX, transform.position, Quaternion.identity);
        Destroy(ps, 1f);

        Destroy(gameObject);
    }

    protected void OnTriggerEnter(Collider other)
    {
        CookingDevice cookingDevice = other.GetComponent<CookingDevice>();

        if (cookingDevice)
        {
            // Add ingredient to ingredient manager
            if (!connectedCookingDevice.Contains(cookingDevice))
            {
                connectedCookingDevice.Add(cookingDevice);
            }

            // Trigger enter sound
            if (cookingDevice.cookingSFX)
            {
                cookingDevice.PlaySFX(cookingDevice.cookingSFX);
            }

            // Trigger enter fx
            if (cookingDevice.cookingVFX)
            {
                cookingDevice.PlayVFX(cookingDevice.cookingVFX);
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        Pan cookingDevice = other.GetComponent<Pan>();

        if (cookingDevice)
        {
            // Remove ingredient from ingredient manager
            if (connectedCookingDevice.Contains(cookingDevice))
            {
                connectedCookingDevice.Remove(cookingDevice);
            }

            // Trigger enter sound

            // Trigger enter fx
        }
    }

    protected void OnDestroy()
    {
        IngredientManager.Instance.RemoveIngredient(this);
    }
}