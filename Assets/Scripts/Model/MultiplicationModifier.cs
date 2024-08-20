using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "AlchemyScale/Multiplication Modifier")]
    public class MultiplicationModifier : Ingredient
    {
        public override int CalculateWeight(List<Ingredient> previousIngredients) => 0;
    }
}
