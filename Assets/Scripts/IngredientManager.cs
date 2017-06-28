using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IngredientManager : MonoBehaviour
{
    private static IngredientManager instance;
    public static IngredientManager Instance { get { return instance; } }

    public CookingDevice currentCookingDevice;
    private List<Ingredient> ingredients = new List<Ingredient>();

    protected void Start()
    {
        InitialiseIngredientManager();
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
}