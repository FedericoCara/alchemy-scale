using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class RecipeTester : MonoBehaviour
{
    public List<IngredientList> recipes;
    void OnEnable()
    {
        foreach (var recipe in recipes)
        {
            var value = ResolveWeight.CalculateWeight(recipe.list).ToString("00");
            Debug.Log($"Result: {value}");
        }
    }
    
    [Serializable]
    public class IngredientList
    {
        public List<Ingredient> list;
    }
}
