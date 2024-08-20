using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "AlchemyScale/Ingredient")]
    public class LiveIngredient : Ingredient
    {
        [SerializeField] private List<Sinergy> sinergies;
        public List<Sinergy> Synergies => sinergies;

        public override int CalculateWeight(List<Ingredient> previousIngredients)
        {
            var resultingWeight = weight;
            foreach (var sinergy in sinergies)
            {
                resultingWeight += sinergy.CalcAddedWeight(previousIngredients);
            }

            return resultingWeight;
        }

        public override bool HasSynergy(List<Ingredient> previousIngredients)
        {
            foreach (var sinergy in sinergies)
            {
                if (sinergy.Activates(previousIngredients))
                    return true;
            }
            return false;
        }
    }
}