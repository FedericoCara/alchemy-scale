using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class Cauldron : MonoBehaviour
    {
        public List<Ingredient> ingredients;

        public MixResult Mix()
        {
            int resultingWeight = 0;
            List<Ingredient> previousIngredients = new List<Ingredient>(ingredients.Count);
            foreach (var ingredient in ingredients)
            {
                resultingWeight += ingredient.CalculateWeight(previousIngredients);
                previousIngredients.Add(ingredient);
            }

            return new MixResult(resultingWeight);
        }
    }

    public class MixResult
    {
        private readonly int _resultingWeight;

        public MixResult(int resultingWeight)
        {
            _resultingWeight = resultingWeight;
        }
    }
}