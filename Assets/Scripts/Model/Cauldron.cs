using System.Collections.Generic;
using Components;

namespace Model
{
    public class Cauldron
    {
        public MixResult Mix(List<Ingredient> ingredients)
        {
            return new MixResult(ResolveWeight.CalculateWeight(ingredients));
        }
    }
}