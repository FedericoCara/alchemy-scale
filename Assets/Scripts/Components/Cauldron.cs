using System.Collections.Generic;
using Model;
using UnityEngine;

namespace Components
{
    public class Cauldron : MonoBehaviour
    {
        public List<Ingredient> ingredients;

        public MixResult Mix()
        {
            int resultingWeight = 0;
            var previousIngredients = new List<Ingredient>(ingredients.Count);
            foreach (var ingredient in ingredients)
            {
                resultingWeight += ingredient.CalculateWeight(previousIngredients);
                previousIngredients.Add(ingredient);
            }

            return new MixResult(resultingWeight);
        }
    }
}