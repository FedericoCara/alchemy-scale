using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Model
{
    [Serializable]
    public class Sinergy
    {
        public Ingredient ingredient;
        public int amount = 1;
        public int weightAdded;
        public bool repeatable;

        public int CalcAddedWeight(List<Ingredient> previousIngredients)
        {
            var addedWeight = 0;
            var matchesCount = 0;
            foreach (var previousIngredient in previousIngredients)
            {
                if (previousIngredient == ingredient)
                {
                    matchesCount++;
                    if (matchesCount >= amount)
                    {
                        GameManager.glossary.Unlock(this);
                        matchesCount = 0;
                        addedWeight += weightAdded;
                        if (!repeatable)
                            return addedWeight;
                    }
                }
            }

            return addedWeight;
        }

        public bool Activates(List<Ingredient> previousIngredients)
        {
            foreach (var previousIngredient in previousIngredients)
            {
                if (previousIngredient == ingredient)
                {
                    return true;
                }
            }
            return false;
        }
    }
}