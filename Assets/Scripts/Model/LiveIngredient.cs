using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "AlchemyScale/Ingredient")]
    public class LiveIngredient : Ingredient
    {
        [SerializeField] private int weight;
        [SerializeField] private List<Sinergy> sinergies;

        public override int CalculateWeight(List<Ingredient> previousIngredients)
        {
            var resultingWeight = weight;
            foreach (var sinergy in sinergies)
            {
                weight += sinergy.CalcAddedWeight(previousIngredients);
            }

            return resultingWeight;
        }
    }
}