using System.Collections;
using System.Collections.Generic;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SynergyView : MonoBehaviour
{
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private Image ingredientImage;
    [SerializeField] private Image targetIngredientImage;
    [SerializeField] private Sprite unknownIngredientSprite;
    
    private Sinergy synergy;
    
    public void Init(Ingredient ingredient, Sinergy synergy)
    {
        this.synergy = synergy;
        
        resultText.text = !GameManager.glossary.IsUnlocked(synergy) 
            ? "?" 
            : $"{synergy.weightAdded}";
        
        targetIngredientImage.sprite = GameManager.glossary.IsUnlocked(synergy.ingredient) 
            ? synergy.ingredient.IconSprite 
            : unknownIngredientSprite;
        
        ingredientImage.sprite = GameManager.glossary.IsUnlocked(ingredient) 
            ? ingredient.IconSprite 
            : unknownIngredientSprite;
        

    }
}
