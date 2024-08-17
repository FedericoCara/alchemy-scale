using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using UI;
using UnityEngine;

public class GlossaryView : MonoBehaviour
{
    [SerializeField] private List<Ingredient> Ingredients;
    [SerializeField] private IngredientView IngredientViewPrefab;
    private List<IngredientView> IngredientViews;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        IngredientViews = new List<IngredientView>();
        foreach (var ingredient in Ingredients)
        {
            var ingredientView = Instantiate(IngredientViewPrefab, transform);
            ingredientView.Init(ingredient);
            IngredientViews.Add(ingredientView);
        }
    }
}
