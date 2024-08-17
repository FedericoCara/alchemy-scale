using System.Collections.Generic;
using Model;
using UnityEngine;

namespace Components
{
    public class Cauldron : MonoBehaviour
    {
        public List<Ingredient> ingredients;

        public MixResult Mix()
        {
            return new MixResult(ResolveWeight.CalculateWeight(ingredients));
        }
    }
}