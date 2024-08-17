using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class Plate : MonoBehaviour
    {
        public List<Ingredient> slots;

        public int CalculateWeight()
        {
            var plateWeight = 0;
            List<Ingredient> previousIngredients = new List<Ingredient>(slots.Count);
            foreach (var ingredient in slots)
            {
                ingredient.CalculateWeight(previousIngredients);
                previousIngredients.Add(ingredient);
            }

            return plateWeight;
        }
    }
}