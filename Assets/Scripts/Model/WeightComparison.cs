namespace Model
{
    public class WeightComparison
    {
        public readonly int subtraction;
        public readonly int sum;
        public readonly int multiplication;

        public WeightComparison(int leftWeight, int rightWeight)
        {
            subtraction = leftWeight - rightWeight;
            sum = leftWeight + rightWeight;
            multiplication = leftWeight * rightWeight;
        }
    }
}