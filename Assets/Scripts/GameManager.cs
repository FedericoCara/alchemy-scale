using Components;
using Model;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private IngredientsManager ingredientsManager;
    [SerializeField] private GameObject successGO;
    [SerializeField] private GameObject failGO;

    public void SetSuccess()
    {
        successGO.SetActive(true);
        levelManager.GetAndSetNextLevel();
        ingredientsManager.RestartIngredients();
    }

    public void SetFail()
    {
        failGO.SetActive(true);
        ingredientsManager.RestartIngredients();
    }
}
