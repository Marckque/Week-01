using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ingredient : MonoBehaviour
{
    protected float cookingValue;
    protected List<CookingDevice> connectedCookingDevice = new List<CookingDevice>();
    public List<CookingDevice> ConnectedCookingDevice { get { return connectedCookingDevice; } }
    public Transform cookingDeviceRoot { get; set; }
    public Transform ingredientSpawnerRoot { get; set; }

    public void ChangeToNewParent()
    {
        transform.SetParent
            (transform.parent == cookingDeviceRoot ? ingredientSpawnerRoot : cookingDeviceRoot);
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