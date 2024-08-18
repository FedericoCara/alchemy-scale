using System.Collections.Generic;
using Model;
using UnityEngine;

namespace Components.Cauldron
{
    public class CauldronWorld : MonoBehaviour
    {
        public List<Ingredient> ingredients;
        public LevelManager levelManager;
        public IngredientsManager ingredientsManager;
        public GameManager gameManager;
        public GameObject mixButton;
        public CameraAnimator camAnimator;
        public CauldronAnimator cauldronAnimator;
        public ScaleController scaleController;

        private Model.Cauldron _cauldron = new();
        private MixResult _lastMixResult;

        public void Mix()
        {
            scaleController.Reset();
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
            _lastMixResult = _cauldron.Mix(ingredients);
            cauldronAnimator.AnimateSpin();
            ingredientsManager.ClearIngredients();
            Invoke(nameof(StopCauldron), 3);
            Invoke(nameof(OnFeedbackFinished), 5);
        }

        private void StopCauldron()
        {
            cauldronAnimator.StopSpinning();
            if (IsSuccess(_lastMixResult))
            {
                gameManager.SetSuccess();
            }
            else
            {
                gameManager.SetFail();
            }
        }

        public void OnFeedbackFinished()
        {
            camAnimator.ReturnToMainPosition();
            if (mixButton != null)
                mixButton.gameObject.SetActive(true);
        }

        private bool IsSuccess(MixResult result)
        {
            return levelManager.CurrentLevel.targetWeight == result.resultingWeight;
        }
    }
}