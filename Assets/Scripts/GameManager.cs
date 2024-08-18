using Components;
using Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private IngredientsManager ingredientsManager;
    [SerializeField] private GameObject successGO;
    [SerializeField] private GameObject failGO;

    public void SetSuccess()
    {
        var nextLevel = levelManager.GetAndSetNextLevel();
        if (nextLevel == null)
        {
            SceneManager.LoadScene("Credits");
            return;
        }

        successGO.SetActive(true);
        ingredientsManager.RestartIngredients();
    }

    public void SetFail()
    {
        failGO.SetActive(true);
        ingredientsManager.RestartIngredients();
    }
}
