using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class Ingredient : MonoBehaviour
{
    [Header("Ingredient")]
    [Header("VFX")]
    public ParticleSystem ateVFX;

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
        ParticleSystem ps = Instantiate(ateVFX, transform.position, Quaternion.identity);
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

            if ((!sourceStartCookingSFX.isPlaying && timeWithoutConnectedDevice > .8f) || playOnce)
            {
                playOnce = false;
                StartCoroutine(PlaySFX());
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

    private IEnumerator PlaySFX()
    {
        float timeStart = Time.time;
        //float startVolume = playOnce == true ? 0.5f : 1f;
        float startVolume = ConnectedCookingDevice.Count > 0 ? ExtensionMethods.Remap(ConnectedCookingDevice[0].heatingPower, 0f, 5f, 0.05f, 0.6f) : 0f;
        float endVolume = 0f;
        float percentageComplete = 0f;

        sourceStartCookingSFX.Play();

        while (percentageComplete < 1f)
        {
            percentageComplete = (Time.time - timeStart) / sourceStartCookingSFX.clip.length;
            sourceStartCookingSFX.volume = Mathf.Lerp(startVolume, endVolume, percentageComplete);
            yield return new WaitForEndOfFrame();
        }
    }

    protected void OnDestroy()
    {
        IngredientManager.Instance.RemoveIngredient(this);
        IngredientManager.Instance.RemoveDyingIngredient(this);
    }
}