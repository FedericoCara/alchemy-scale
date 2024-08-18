using System.Collections.Generic;
using Model;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public class IngredientView : MonoBehaviour
    {
        [SerializeField] private TMP_Text ingredientNameText;
        [SerializeField] private TMP_Text ingredientWeightText;
        [SerializeField] private SynergyView synergyViewPrefab;
        [SerializeField] private Transform synergiesParent;
        [SerializeField] private Image ingredientImage;
        
        private Ingredient ingredient;
        private List<SynergyView> synergiesViews;
        
        public void Init(Ingredient ingredient)
        {
            this.ingredient = ingredient;

            ingredientNameText.text = ingredient.name;
            ingredientWeightText.text = ingredient.Weight.ToString();
            ingredientImage.sprite = ingredient.IconSprite;
            
            synergiesViews = new List<SynergyView>();
            if (ingredient is LiveIngredient liveIngredient)
            {
                foreach (var synergy in liveIngredient.Synergies)
                {
                    var synergyView = Instantiate(synergyViewPrefab, synergiesParent);
                    synergyView.Init(synergy);
                    synergiesViews.Add(synergyView);
                }
            }
        }
        
    }
}
