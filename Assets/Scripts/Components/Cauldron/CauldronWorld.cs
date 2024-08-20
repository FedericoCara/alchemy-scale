using System.Collections.Generic;
using Components.Ingredients;
using DG.Tweening;
using Model;
using UnityEngine;
using FMODUnity;

namespace Components.Cauldron
{
    public class CauldronWorld : MonoBehaviour
    {      
        [SerializeField] private EventReference winAudio;
        [SerializeField] private EventReference defeatAudio;
        public List<Ingredient> ingredients;
        public LevelManager levelManager;
        public IngredientsManager ingredientsManager;
        public GameManager gameManager;
        public GameObject mixButton;
        public CameraAnimator camAnimator;
        public CauldronAnimator cauldronAnimator;
        public CauldronPreview cauldronPreview;
        public ScaleController scaleController;
        public PotionSpawner potionSpawner;
        public CauldronButtonController cauldronButtonController;
        public float resetCamTime = 7;
        

        private Model.Cauldron _cauldron = new();
        private MixResult _lastMixResult;

        private void OnEnable()
        {
            DisableMixButton();
            EnableCauldronPreview(false);
        }

        private void EnableCauldronPreview(bool enabled)
        {
            cauldronPreview.gameObject.SetActive(enabled);
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
            EnableCauldronPreview(true);
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
            cauldronAnimator.AnimateSpin(IsSuccess(_lastMixResult), StopCauldron);
            ingredientsManager.ClearIngredients();
        }

        private void StopCauldron()
        {
            EmptyIngredients();
            if (IsSuccess(_lastMixResult))
            {
                potionSpawner.SpawnPotion(OnPotionShown);
                RuntimeManager.PlayOneShot(winAudio);
            }
            else
            {
                Debug.Log("Cauldron failed with weight: "+_lastMixResult.resultingWeight);
                RuntimeManager.PlayOneShot(defeatAudio);
                gameManager.SetFail();
                OnFeedbackFinished();
            }
        }

        private void OnPotionShown()
        {
            gameManager.SetSuccess();
            OnFeedbackFinished();
        }

        private void EmptyIngredients()
        {
            cauldronPreview.Clear();
            ingredients.Clear();
            EnableCauldronPreview(false);
        }

        public void OnFeedbackFinished()
        {
            camAnimator.ReturnToMainPosition();
        }

        private void EnableMixButton()
        {
            cauldronButtonController.Interactable = true;
        }

        private void DisableMixButton()
        {
            cauldronButtonController.Interactable = false;
        }

        private bool IsSuccess(MixResult result)
        {
            return levelManager.CurrentLevel.targetWeight == result.resultingWeight;
        }
    }
}