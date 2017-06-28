using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class Ingredient : MonoBehaviour
{
    [Header("Ingredient")]
    [Header("Feedback")]
    public ParticleSystem destroyedVFX;
    public ParticleSystem cookingVFX;
    public AudioSource audioSource;
    public AudioClip cookingSFX;

    protected float cookingValue;
    protected List<CookingDevice> connectedCookingDevice = new List<CookingDevice>();
    public List<CookingDevice> ConnectedCookingDevice { get { return connectedCookingDevice; } }
    public Transform cookingDeviceRoot { get; set; }
    public Transform ingredientSpawnerRoot { get; set; }
    private Vector3 endPosition;
    protected Rigidbody entityRigidbody;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
                if (audioSource.clip != cookingDevice.cookingSFX)
                {
                    audioSource.clip = cookingDevice.cookingSFX;
                }

                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }

            // Trigger enter fx
            if (cookingDevice.cookingVFX)
            {
                if (cookingVFX != cookingDevice.cookingVFX)
                {
                    cookingVFX = cookingDevice.cookingVFX;
                }
                
                cookingVFX.Play();
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        CookingDevice cookingDevice = other.GetComponent<CookingDevice>();

        if (cookingDevice)
        {
            // Remove ingredient from ingredient manager
            if (connectedCookingDevice.Contains(cookingDevice))
            {
                connectedCookingDevice.Remove(cookingDevice);
            }
        }
    }

    protected void OnDestroy()
    {
        IngredientManager.Instance.RemoveIngredient(this);
    }
}