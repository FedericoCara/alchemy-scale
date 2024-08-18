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
    [SerializeField] private Transform ingredientViewParent;
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
            var ingredientView = Instantiate(IngredientViewPrefab, ingredientViewParent);
            ingredientView.Init(ingredient);
            IngredientViews.Add(ingredientView);
        }
    }
}
