using System;
using System.Collections.Generic;
using Components;
using Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private IngredientsManager ingredientsManager;
    [SerializeField] private CameraAnimator cameraAnimator;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private TutorialController tutorialController;

    public String[] tutorialTitles;
    public String[] tutorialTexts;

    public static Glossary glossary = new Glossary();
    public List<LiveIngredient> allIngredients;

    public void Awake()
    {
        glossary.Unlock(0);
        glossary.Unlock(allIngredients[0]);

        tutorialTitles = new String[5]
        {
            "The basics of making potions:",
            "Magical reactions:",
            "Modifying the count:",
            "Subtracting the value:",
            "Doubling the value:"
        };

        tutorialTexts = new String[5]
        {
            "\tA well made potion requires that all of its ingredients weights add up to a specified amount.\n\n\tWhen you put ingredients into the pot, you're adding up the weight of all those ingredients in order.\n\n\tFor example, two tentacles are just 10 + 10 = 20 but be careful, not all of them are going to be that simple.",
            "\tCertain ingredients work well together. Putting one of the specified ingredients before the current one can trigger its synergy. Some items only do it once, but others can do it for every item that is repeated in the chain.\n\n\tAn easy example is a tentacle and an eye. That gives us 10 + 25, but eyes make a reaction when a tentacle is anywhere on its left so in this case it subtracts from the weight, making the real value be 10 + \"25 - 10\" = 25.\n\n\tSo if you put them in the inverse order, the synergy for the eye isn't going to be triggered, but the one for the tentacle will.",
            "\tThere are special stones that don't add weight to the sum, they work in a different manner.\n\n\tThink of them as mathematical operators.",
            "\tA green gem acts like a minus operator. When they are added, it subtracts everything on its right to its left.\n\n\tFor example, two tentacles, a gem and three more tentacles would be like this: 10 + 10 - (10 + 10 + 10) = -10. So be careful with how you place your parenthesis.\n\n\tHave in mind that two consecutive gems don't invert the sign of operation.",
            "\tA blue diamond doubles everything on its left. So if you have two tentacles, a diamond and a mushroom you get: (10 + 10) * 2 + 75 = 115.\n\n\tOher example, if you have a tentacle, a diamond, other tentacles and other demands you get ((10) * 2 + 10) * 2 = 60. Be mindful of how you calculate your numbers."
        };
    }

    public void StartGame()
    {
        ingredientsManager.RestartIngredients();
    }

    public void OnTutorialFinished()
    {
        ingredientsManager.RestartIngredients();
    }

    public void SetSuccess()
    {
        switch (levelManager.currentLevelIndex)
        {
            case 0:
                {
                    glossary.UnlockWeight(allIngredients[0]);
                    glossary.Unlock(allIngredients[1]);
                }
                break;

            case 1:
                {
                    glossary.UnlockWeight(allIngredients[1]);
                    glossary.Unlock(allIngredients[1].Synergies[0]);
                    glossary.Unlock(1);
                }
                break;

            case 2:
                {
                    glossary.Unlock(allIngredients[0].Synergies[0]);
                }
                break;

            case 3:
                {
                    glossary.Unlock(allIngredients[2]);
                }
                break;

            case 4:
                {
                    glossary.UnlockWeight(allIngredients[2]);
                }
                break;

            case 5:
                {
                    glossary.Unlock(allIngredients[2].Synergies[0]);
                }
                break;

            case 6:
                {
                    glossary.Unlock(allIngredients[4]);
                }
                break;

            case 7:
                {
                    glossary.UnlockWeight(allIngredients[4]);
                }
                break;

            case 11:
                {
                    glossary.Unlock(allIngredients[3]);
                }
                break;

            case 12:
                {
                    glossary.Unlock(2);
                    glossary.UnlockWeight(allIngredients[3]);
                }
                break;

            case 13:
                {
                    glossary.Unlock(3);
                }
                break;

            case 15:
                {
                    glossary.Unlock(allIngredients[5]);
                }
                break;

            case 17:
                {
                    glossary.UnlockWeight(allIngredients[5]);
                }
                break;

            case 18:
                {
                    glossary.Unlock(allIngredients[4].Synergies[0]);
                }
                break;

            case 19:
                {
                    glossary.Unlock(allIngredients[5].Synergies[0]);
                }
                break;

            case 20:
                {
                    glossary.Unlock(4);
                }
                break;
        }

        //levelManager.CurrentLevel.startingIngredients.ForEach(item => glossary.UnlockWeight(item.ingredient));
        var nextLevel = levelManager.GetAndSetNextLevel();
        if (nextLevel == null)
        {
            cameraAnimator.MoveToTutorialPosition();
            creditsPanel.SetActive(true);
            tutorialController.gameObject.SetActive(false);
            levelManager.ResetGame();
            return;
        }

        //nextLevel.startingIngredients.ForEach(item => glossary.Unlock(item.ingredient));
        ingredientsManager.RestartIngredients();
    }

    public void SetFail()
    {
        ingredientsManager.RestartIngredients();
    }

    public int GetCurrentLevel()
    {
        return levelManager.currentLevelIndex;
    }
}
