using System.Collections.Generic;
using Model;
using UnityEngine;

namespace Components.Cauldron
{
    public class CauldronWorld : MonoBehaviour
    {
        public List<Ingredient> ingredients;

        public MixResult Mix()
        {
            return new MixResult(ResolveWeight.CalculateWeight(ingredients));
        }

        public void AddIngredient(IngredientWorld ingredient)
        {
            ingredients.Add(ingredient.ingredient);
            ingredient.gameObject.SetActive(false);
        }
    }
}