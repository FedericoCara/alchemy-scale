using System;
using System.Collections.Generic;

namespace Model
{
    [Serializable]
    public class ScaleSide
    {
        public List<Plate> plates;

        public int CalculateWeight()
        {
            int plateWeight = 0;
            foreach (var plate in plates)
            {
                plateWeight += plate.CalculateWeight();
            }

            return plateWeight;
        }
    }
}