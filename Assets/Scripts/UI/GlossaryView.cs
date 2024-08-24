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
    }

    public void PlayTheSound()
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 1f);
    }
}
