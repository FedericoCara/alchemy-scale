using Model;
using TMPro;
using UnityEngine;
namespace UI
{
    public class IngredientView : MonoBehaviour
    {
        [SerializeField] private TMP_Text ingredientNameText;
        [SerializeField] private TMP_Text ingredientWeightText;
        
        private Ingredient ingredient;
        
        public void Init(Ingredient ingredient)
        {
            this.ingredient = ingredient;

            ingredientNameText.text = ingredient.name;
            ingredientWeightText.text = ingredient.Weight.ToString();
        }
        
    }
}
