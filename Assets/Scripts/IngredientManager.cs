using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IngredientManager : MonoBehaviour
{
    private static IngredientManager instance;
    public static IngredientManager Instance { get { return instance; } }

    public CookingDevice currentCookingDevice;
    private List<Ingredient> ingredients = new List<Ingredient>();
    private List<Ingredient> dyingIngredients = new List<Ingredient>();
    public List<Ingredient> DyingIngredients { get { return dyingIngredients; } }

    public AudioSource squeezeAudioSource;
    public AudioClip[] squeezeClips;

    public float TimeLastSqueeze { get; set; }
    public float TimeBetweenEachSqueezes { get; set; }
    public bool FirstSqueeze { get; set; }


    protected void Start()
    {
        InitialiseIngredientManager();
        TimeBetweenEachSqueezes = 5f;
    }

    private void InitialiseIngredientManager()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        if (instance == null || instance != this)
        {
            Debug.LogError("Error with the initialisation of the GameManagement");
        }
    }

    public void AddIngredient(Ingredient ingredient)
    {
        if (!ingredients.Contains(ingredient))
        {
            ingredients.Add(ingredient);
        }
    }

    public void RemoveIngredient(Ingredient ingredient)
    {
        if (ingredients.Contains(ingredient))
        {
            ingredients.Remove(ingredient);
        }
    }

    public void AddDyingIngredient(Ingredient ingredient)
    {
        if (!dyingIngredients.Contains(ingredient))
        {
            dyingIngredients.Add(ingredient);
        }
    }

    public void RemoveDyingIngredient(Ingredient ingredient)
    {
        if (dyingIngredients.Contains(ingredient))
        {
            dyingIngredients.Remove(ingredient);
        }
    }

    public void SetKinematic(bool value)
    {
        foreach (Ingredient ingredient in ingredients)
        {
            ingredient.SetIsKinematic(value);
        }
    }

    public void SwitchParents(bool value)
    {
        foreach (Ingredient ingredient in ingredients)
        {
            ingredient.ChangeToNewParent(value);
        }
    }

    public void Eat()
    {
        if (ingredients.Count > 0)
        {
            ingredients[0].EatIngredient();
        }
    }

    public void PlaySqueeze()
    {
        if (!squeezeAudioSource.isPlaying)
        {
            StartCoroutine(PlaySFX());
        }
    }

    private IEnumerator PlaySFX()
    {
        float timeStart = Time.time;
        float startVolume = 0.7f;
        float endVolume = 0f;
        float percentageComplete = 0f;

        int randomClip = Random.Range(0, squeezeClips.Length);
        squeezeAudioSource.clip = squeezeClips[randomClip];

        squeezeAudioSource.pitch = 1f + Random.Range(-0.05f, 0.05f);
        squeezeAudioSource.Play();

        while (percentageComplete < 1f)
        {
            percentageComplete = (Time.time - timeStart) / 1f;
            squeezeAudioSource.volume = Mathf.Lerp(startVolume, endVolume, percentageComplete);
            yield return new WaitForEndOfFrame();
        }
    }
}