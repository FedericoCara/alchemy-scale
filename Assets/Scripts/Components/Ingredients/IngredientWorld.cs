using Model;
using UnityEngine;

namespace Components.Ingredients
{
    public class IngredientWorld : MonoBehaviour
    {
        public Ingredient ingredient;
        public GameObject synergy;

        public void TurnOnSynergy()
        {
            synergy.SetActive(true);
        }

        public void TurnOffSynergy()
        {
            synergy.SetActive(false);
        }
    }
}
