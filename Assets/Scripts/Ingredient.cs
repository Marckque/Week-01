using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class Ingredient : MonoBehaviour
{
    [Header("Ingredient")]
    [Header("VFX")]
    public ParticleSystem destroyedVFX;
    public ParticleSystem cookingVFX;

    [Header("Audio")]
    public AudioSource sourceStartCookingSFX;
    public AudioSource sourceCookingSFX;

    protected float cookingValue;
    protected List<CookingDevice> connectedCookingDevice = new List<CookingDevice>();
    public List<CookingDevice> ConnectedCookingDevice { get { return connectedCookingDevice; } }
    public Transform cookingDeviceRoot { get; set; }
    public Transform ingredientSpawnerRoot { get; set; }
    private Vector3 endPosition;
    protected Rigidbody entityRigidbody;
    protected float timeWithoutConnectedDevice;

    private bool playOnce = true;

    protected virtual void Start()
    {
        entityRigidbody = GetComponent<Rigidbody>();
        endPosition = new Vector3(0f, 3f, -15f);
    }

    protected virtual void Update()
    {
        /*
        if (ConnectedCookingDevice.Count > 0)
        {
            if (!sourceCookingSFX.isPlaying)
            {
                sourceCookingSFX.Play();
            }
        }
        */
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

            // !!!!!!!!!!!! A REPARER !!!!!!!!!!
            // !!!!!!!!!!!! A REPARER !!!!!!!!!!
            // !!!!!!!!!!!! A REPARER !!!!!!!!!!
            // !!!!!!!!!!!! A REPARER !!!!!!!!!!
            // !!!!!!!!!!!! A REPARER !!!!!!!!!!

            // Trigger enter sound 
            //if (cookingDevice.sourceStartCookingSFX)
            {
                //if (sourceStartCookingSFX.clip != cookingDevice.sourceStartCookingSFX)
                //{
                //    sourceStartCookingSFX.clip = cookingDevice.sourceStartCookingSFX;
                //}

                if ((!sourceStartCookingSFX.isPlaying && timeWithoutConnectedDevice > .8f) || playOnce)
                {
                    playOnce = false;
                    sourceStartCookingSFX.Play();
                }
            }

            // Trigger enter fx
            if (cookingDevice.cookingVFX)
            {
                if (cookingVFX != cookingDevice.cookingVFX)
                {
                    cookingVFX = cookingDevice.cookingVFX;
                }
                
                if (!cookingVFX.isPlaying)
                {
                    /*
                    ParticleSystem ps = Instantiate(cookingVFX, transform.position, Quaternion.identity);

                    ps.transform.localEulerAngles = Vector3.zero;
                    ps.transform.position = Vector3.zero;
                    */

                    //float y = other.ClosestPoint(transform.position).y + 0.05f;

                    //cookingVFX.transform.position = new Vector3(transform.position.x, y, transform.position.z);
                    //cookingVFX.Play();
                    //cookingVFX.Emit(0);
                }
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

            if (cookingVFX.isPlaying)
            {
                cookingVFX.Stop();
                cookingVFX.Pause();
            }
        }
    }

    protected void OnDestroy()
    {
        IngredientManager.Instance.RemoveIngredient(this);
    }
}