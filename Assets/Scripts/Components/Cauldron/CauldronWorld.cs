using System;
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
        public CauldronPreview cauldronPreview;
        public ScaleController scaleController;
        public float mixTime = 5;
        public float resetCamTime = 7;

        private Model.Cauldron _cauldron = new();
        private MixResult _lastMixResult;

        private void OnEnable()
        {
            DisableMixButton();
        }

        public void Mix()
        {
            scaleController.Reset();
            DisableMixButton();
            AnimateStartMix();
        }

        public void AddIngredient(IngredientWorld ingredient)
        {
            ingredients.Add(ingredient.ingredient);
            cauldronPreview.Add(ingredient.ingredient);
            ingredient.gameObject.SetActive(false);
            EnableMixButton();
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
            Invoke(nameof(StopCauldron), mixTime);
            Invoke(nameof(OnFeedbackFinished), resetCamTime);
        }

        private void StopCauldron()
        {
            cauldronAnimator.StopSpinning();
            EmptyIngredients();
            if (IsSuccess(_lastMixResult))
            {
                gameManager.SetSuccess();
            }
            else
            {
                Debug.Log("Cauldron failed with weight: "+_lastMixResult.resultingWeight);
                gameManager.SetFail();
            }
        }

        private void EmptyIngredients()
        {
            cauldronPreview.Clear();
            ingredients.Clear();
        }

        public void OnFeedbackFinished()
        {
            camAnimator.ReturnToMainPosition();
        }

        private void EnableMixButton()
        {
            if (mixButton != null)
                mixButton.gameObject.SetActive(true);
        }

        private void DisableMixButton()
        {
            if (mixButton != null)
                mixButton.gameObject.SetActive(false);
        }

        private bool IsSuccess(MixResult result)
        {
            return levelManager.CurrentLevel.targetWeight == result.resultingWeight;
        }
    }
}