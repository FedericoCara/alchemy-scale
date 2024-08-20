using System.Collections.Generic;

namespace Model
{
    public static class ResolveWeight
    {
        public static int CalculateWeight(List<Ingredient> ingredients)
        {
            var totalWeight = 0;
            List<Ingredient> previousIngredients = new List<Ingredient>(ingredients.Count);
            OperatorMode operatorMode = OperatorMode.SUM;
            foreach (var ingredient in ingredients)
            {
                var previousMode = operatorMode;
                operatorMode = HandleOperator(operatorMode, ingredient);
                switch (operatorMode)
                {
                    case OperatorMode.SUM:
                        totalWeight += ingredient.CalculateWeight(previousIngredients);
                        break;
                    case OperatorMode.SUBTRACT:
                        totalWeight -= ingredient.CalculateWeight(previousIngredients);
                        break;
                    case OperatorMode.MULTIPLICATION:
                        totalWeight *= ingredient.CalculateWeight(previousIngredients);
                        operatorMode = previousMode;
                        break;
                }
                previousIngredients.Add(ingredient);
            }

            return totalWeight;
        }

        private static OperatorMode HandleOperator(OperatorMode currentOperator, Ingredient ingredient)
        {
            if (ingredient is SubtractionModifier)
                return OperatorMode.SUBTRACT;
            if (ingredient is MultiplicationModifier)
                return OperatorMode.MULTIPLICATION;
            return currentOperator;
        }

        private enum OperatorMode
        {
            SUM,
            SUBTRACT,
            MULTIPLICATION
        }
    }
}