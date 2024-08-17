using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public abstract class Ingredient : ScriptableObject
    {
        [SerializeField] protected int weight;
        public int Weight => weight;
        
        public abstract int CalculateWeight(List<Ingredient> previousIngredients);
    }
}