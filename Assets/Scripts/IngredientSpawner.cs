using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IngredientSpawner : MonoBehaviour
{
    public Transform cookingDeviceRoot;
    public Ingredient ingredientToSpawn;
    public float cooldown;
    private float latestSpawnTime;

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && Time.time > latestSpawnTime + cooldown)
        {
            latestSpawnTime = Time.time;

            Vector3 randomRotation = ExtensionMethods.RandomUnifiedVector3(360f);
            Vector3 randomPosition = ExtensionMethods.RandomVector3();

            float scale = Random.Range(0.5f, 1f);
            Vector3 randomScale = new Vector3(scale, scale, scale);

            Ingredient ingredient = Instantiate(ingredientToSpawn, new Vector3(0f, 7.5f, 0f), Quaternion.identity);

            ingredient.transform.position += randomPosition;
            ingredient.transform.eulerAngles = randomRotation;
            ingredient.transform.localScale = randomScale;           
            ingredient.transform.SetParent(transform);

            ingredient.cookingDeviceRoot = cookingDeviceRoot;
            ingredient.ingredientSpawnerRoot = transform;

            IngredientManager.Instance.AddIngredient(ingredient);
        }
    }
}