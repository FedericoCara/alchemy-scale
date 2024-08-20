using Components.Ingredients;
using Model;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components.Cauldron
{
    public class OnIngredientEnteringCauldron : MonoBehaviour
    {
        [SerializeField] private CauldronWorld cauldron;
        private void OnTriggerEnter(Collider other)
        {
            var ingredient = other.GetComponentInParent<IngredientWorld>();
            if (ingredient != null)
            {
                cauldron.AddIngredient(ingredient);
            }
        }
    }
}
