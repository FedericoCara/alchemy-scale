using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public class IngredientView : MonoBehaviour
    {
        [SerializeField] private GameObject ingredientPage;
        [SerializeField] private GameObject tutorialPage;

        [SerializeField] private TMP_Text tutorialTitleText;
        [SerializeField] private TMP_Text tutorialText;

        [SerializeField] private TMP_Text ingredientNameText;
        [SerializeField] private TMP_Text ingredientWeightText;
        [SerializeField] private SynergyView synergyViewPrefab;
        [SerializeField] private Transform synergiesParent;
        [SerializeField] private Image ingredientImage;
        [SerializeField] private Image ingredientNameImage;

        private GameManager GetGameManager;

        private Ingredient ingredient;
        private List<SynergyView> synergiesViews; private void Awake()
        {
            GetGameManager = FindObjectOfType<GameManager>();
        }

        public void Init(Ingredient ingredient)
        {
            this.ingredient = ingredient;

            ingredientNameText.text = ingredient.name;
            ingredientWeightText.text = GameManager.glossary.IsUnlockedWeight(ingredient)
                ? ingredient.Weight.ToString()
                : "?";
            ingredientImage.sprite = ingredient.IconSprite;
            ingredientNameImage.sprite = ingredient.IconSprite;
            
            synergiesViews = new List<SynergyView>();
            if (ingredient is LiveIngredient liveIngredient)
            {
                foreach (var synergy in liveIngredient.Synergies)
                {
                    var synergyView = Instantiate(synergyViewPrefab, synergiesParent);
                    synergyView.Init(ingredient, synergy);
                    synergiesViews.Add(synergyView);
                }
            }
        }

        public void InitTutorialPage(int tutorialPageIndex)
        {
            ingredientPage.SetActive(false);
            tutorialPage.SetActive(true);

            tutorialTitleText.text = GetGameManager.tutorialTitles[tutorialPageIndex];
            tutorialText.text = GetGameManager.tutorialTexts[tutorialPageIndex];
        }
    }
}
