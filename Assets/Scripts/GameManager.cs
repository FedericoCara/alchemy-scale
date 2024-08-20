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

    public static Glossary glossary = new Glossary();

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
        levelManager.CurrentLevel.startingIngredients.ForEach(item => glossary.UnlockWeight(item.ingredient));
        var nextLevel = levelManager.GetAndSetNextLevel();
        if (nextLevel == null)
        {
            cameraAnimator.MoveToTutorialPosition();
            creditsPanel.SetActive(true);
            tutorialController.gameObject.SetActive(false);
            levelManager.ResetGame();
            return;
        }

        nextLevel.startingIngredients.ForEach(item => glossary.Unlock(item.ingredient));
        ingredientsManager.RestartIngredients();
    }

    public void SetFail()
    {
        ingredientsManager.RestartIngredients();
    }
}
