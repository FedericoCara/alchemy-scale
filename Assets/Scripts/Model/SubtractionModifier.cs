using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "AlchemyScale/Subtraction Modifier")]
    public class SubtractionModifier : Ingredient
    {
        public override int CalculateWeight(List<Ingredient> previousIngredients) => 0;
        public override bool HasSynergy(List<Ingredient> previousIngredients) => false;
    }
}