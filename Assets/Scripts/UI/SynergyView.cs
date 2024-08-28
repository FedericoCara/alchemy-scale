using System.Collections;
using System.Collections.Generic;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SynergyView : MonoBehaviour
{
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text weightLabelText;
    [SerializeField] private Image ingredientImage;
    [SerializeField] private Image targetIngredientImage;
    [SerializeField] private Sprite unknownIngredientSprite;
    [SerializeField] private GameObject simpleTextGroup;
    [SerializeField] private GameObject repetitiveTextGroup;
    
    private Sinergy synergy;
    
    public void Init(Ingredient ingredient, Sinergy synergy)
    {
        this.synergy = synergy;
        
        resultText.text = !GameManager.glossary.IsUnlocked(synergy) 
            ? "? " 
            : $"{(synergy.weightAdded>0?"+":"")}{synergy.weightAdded}";

        if (!GameManager.glossary.IsUnlocked(synergy))
        {
            weightLabelText.text = "? ";
        }
        else
        {
            if (synergy.weightAdded > 0)
            {
                weightLabelText.text = "Added weight";
            }
            else
            {
                weightLabelText.text = "Subtracted weight";
            }
        }

        targetIngredientImage.sprite = GameManager.glossary.IsUnlocked(synergy.ingredient) 
            ? synergy.ingredient.IconSprite 
            : unknownIngredientSprite;
        
        ingredientImage.sprite = GameManager.glossary.IsUnlocked(ingredient) 
            ? ingredient.IconSprite 
            : unknownIngredientSprite;
        
        repetitiveTextGroup.SetActive(this.synergy.repeatable);
        simpleTextGroup.SetActive(!this.synergy.repeatable);
        

    }
}
