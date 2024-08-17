using System;
using System.Collections.Generic;

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
                        matchesCount = 0;
                        addedWeight += weightAdded;
                        if (!repeatable)
                            return addedWeight;
                    }
                }
            }

            return addedWeight;
        }
    }
}