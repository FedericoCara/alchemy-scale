using System;

namespace Model
{
    [Serializable]
    public class Sinergy
    {
        public Ingredient ingredient;
        public int amount;
        public int weightAdded;
        public bool repeatable;
    }
}