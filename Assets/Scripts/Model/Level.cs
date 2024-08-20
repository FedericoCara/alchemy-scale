using System;
using System.Collections.Generic;
using Components;
using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "AlchemyScale/Level")]
    public class Level : ScriptableObject
    {
        public List<StartingIngredient> startingIngredients;
        public int targetWeight;
        public GameObject targetPotion;
        public Sprite targetPotionIcon;
    }

    [Serializable]
    public class StartingIngredient
    {
        public Ingredient ingredient;
        public int amount = 1;
    }
}