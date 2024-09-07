using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using UI;
using UnityEngine;

public class GlossaryView : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    [SerializeField] private List<Ingredient> Ingredients;
    [SerializeField] private IngredientView IngredientViewPrefab;
    [SerializeField] private Transform ingredientViewParent;
    private List<IngredientView> ingredientViews = new();

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        ingredientViews.ForEach(view => Destroy(view.gameObject));
        ingredientViews.Clear();

        // I really hate all of this, sorry.
        int[] pageTypes = new int[16] { 0, -1, -1, 1, -1, -1, -1, -1, -1, -1, -1, 2 ,3 ,-1 ,-1, 4};
        Ingredient[] ingredientsOrder = new Ingredient[16] { null, Ingredients[0], Ingredients[1], null, null, null, Ingredients[2], null, Ingredients[4], null, Ingredients[3], null, null, Ingredients[5], null, null };

        for (int iterator = 0; iterator < 16; iterator++)
        {
            if (pageTypes[iterator] != -1)
            {
                if (!GameManager.glossary.IsUnlocked(pageTypes[iterator]))
                {
                    continue;
                }

                var tutorialView = Instantiate(IngredientViewPrefab, ingredientViewParent);
                tutorialView.InitTutorialPage(pageTypes[iterator]);
                ingredientViews.Add(tutorialView);
            }
            else
            {
                if (ingredientsOrder[iterator] == null || !GameManager.glossary.IsUnlocked(ingredientsOrder[iterator]))
                {
                    continue;
                }

                var ingredientView = Instantiate(IngredientViewPrefab, ingredientViewParent);
                ingredientView.Init(ingredientsOrder[iterator]);
                ingredientViews.Add(ingredientView);
            }
        }
        /*
        foreach (var ingredient in Ingredients)
        {
            if (!GameManager.glossary.IsUnlocked(ingredient))
            {
                continue;
            }
            
            var ingredientView = Instantiate(IngredientViewPrefab, ingredientViewParent);
            ingredientView.Init(ingredient);
            ingredientViews.Add(ingredientView);
        }
        */
    }

    public void PlayTheSound()
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 1f);
    }
}
