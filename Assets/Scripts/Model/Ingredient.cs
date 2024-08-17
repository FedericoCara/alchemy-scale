using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "AlchemyScale/Ingredient")]
    public class Ingredient : ScriptableObject
    {
        [SerializeField] private int weight;
        [SerializeField] private List<Sinergy> sinergies;
        public int Weight => weight;
        public List<Sinergy> Sinergies => new List<Sinergy>(sinergies);

        public int CalculateWeight(List<Ingredient> previousIngredients)
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