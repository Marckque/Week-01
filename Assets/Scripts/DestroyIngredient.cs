using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIngredient : MonoBehaviour
{
    protected void Update()
    {
        if (IngredientManager.Instance.DyingIngredients.Count > 0)
        {
            foreach (Ingredient ingredient in IngredientManager.Instance.DyingIngredients)
            {
                if (ingredient.transform.localScale.x > 0.05f)
                {
                    ingredient.transform.localScale -= (Vector3.one * Time.deltaTime);
                }
                else
                {
                    if (IngredientManager.Instance.DyingIngredients.Contains(ingredient))
                    {
                        Destroy(ingredient.gameObject);
                    }
                }
            }
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        Ingredient ingredient = other.GetComponent<Ingredient>();

        if (ingredient)
        {
            if (!IngredientManager.Instance.DyingIngredients.Contains(ingredient))
            {
                IngredientManager.Instance.AddDyingIngredient(ingredient);
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        Ingredient ingredient = other.GetComponent<Ingredient>();

        if (ingredient)
        {
            if (IngredientManager.Instance.DyingIngredients.Contains(ingredient))
            {
                IngredientManager.Instance.RemoveDyingIngredient(ingredient);
            }
        }
    }
}