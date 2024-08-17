using Model;
using UnityEngine;

namespace Components
{
    public class WeightScale : MonoBehaviour
    {
        public ScaleSide scaleSideLeft;
        public ScaleSide scaleSideRight;

        public WeightComparison CompareWeights()
        {
            int leftWeight = scaleSideLeft.CalculateWeight();
            int rightWeight = scaleSideRight.CalculateWeight();
            return new WeightComparison(leftWeight, rightWeight);
        }
    }
}
