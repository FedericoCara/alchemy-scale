using System.Collections.Generic;
using Model;
using UnityEngine;

namespace Components.Cauldron
{
    public class CauldronWorld : MonoBehaviour
    {
        public List<Ingredient> ingredients;
        public LevelManager levelManager;
        public GameObject mixButton;
        public CameraAnimator camAnimator;
        public CauldronAnimator cauldronAnimator;

        private Model.Cauldron _cauldron = new();

        public void Mix()
        {
            DisableMixButton();
            AnimateStartMix();
        }

        public void AddIngredient(IngredientWorld ingredient)
        {
            ingredients.Add(ingredient.ingredient);
            ingredient.gameObject.SetActive(false);
        }

        private void DisableMixButton()
        {
            if (mixButton != null)
                mixButton.gameObject.SetActive(false);
        }

        private void AnimateStartMix()
        {
            camAnimator.MoveToMixPosition();
        }

        public void OnAnimateStartFinished()
        {
            var result = _cauldron.Mix(ingredients);
            if (IsSuccess(result))
            {
                cauldronAnimator.AnimateSuccess();
            }
            else
            {
                cauldronAnimator.AnimateFail();
            }
        }

        public void OnFeedbackFinished()
        {
            camAnimator.ReturnToMainPosition();
        }

        private bool IsSuccess(MixResult result)
        {
            return levelManager.CurrentLevel.targetWeight == result.resultingWeight;
        }
    }
}