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
    [SerializeField] private TutorialController tutorialController;
    [SerializeField] private GameObject successGO;
    [SerializeField] private GameObject failGO;

    public static Glossary glossary = new Glossary();

    private void Start()
    {
        if (levelManager.LevelNumber == 1)
        {
            tutorialController.ShowTutorial();
        }
        else
        {
            ingredientsManager.RestartIngredients();
        }
    }

    public void OnTutorialFinished()
    {
        ingredientsManager.RestartIngredients();
    }

    public void SetSuccess()
    {
        levelManager.CurrentLevel.startingIngredients.ForEach(item => glossary.UnlockWeight(item.ingredient));
        var nextLevel = levelManager.GetAndSetNextLevel();
        if (nextLevel == null)
        {
            SceneManager.LoadScene("Credits");
            return;
        }

        nextLevel.startingIngredients.ForEach(item => glossary.Unlock(item.ingredient));
        successGO.SetActive(true);
        ingredientsManager.RestartIngredients();
    }

    public void SetFail()
    {
        failGO.SetActive(true);
        ingredientsManager.RestartIngredients();
    }
}
