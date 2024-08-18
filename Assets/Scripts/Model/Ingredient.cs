using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Model
{
    public abstract class Ingredient : ScriptableObject
    {
        [SerializeField] protected Sprite iconSprite;

        [SerializeField] protected int weight;
        public int Weight => weight;
        public Sprite IconSprite => iconSprite;
        
        public abstract int CalculateWeight(List<Ingredient> previousIngredients);
    }
}