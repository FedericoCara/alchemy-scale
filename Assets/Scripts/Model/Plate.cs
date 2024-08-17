using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class Plate : MonoBehaviour
    {
        public List<Ingredient> slots;

        public int CalculateWeight() => 
            ResolveWeight.CalculateWeight(slots);
    }
}