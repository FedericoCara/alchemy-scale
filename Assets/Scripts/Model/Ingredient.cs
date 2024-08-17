using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public abstract class Ingredient : ScriptableObject
    {
        public abstract int CalculateWeight(List<Ingredient> previousIngredients);
    }
}