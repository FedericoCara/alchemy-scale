using System;
using System.Collections.Generic;
using Components;
using Model;
using UnityEngine;

public class ScaleController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private List<Transform> leftPositions;
    [SerializeField] private List<Transform> rightPositions;

    private event Action onTouchLeft;
    private event Action onTouchRight;
    private float result;

    private List<IngredientWorld> ingredientsWorldLeft = new ();
    private List<IngredientWorld> ingredientsWorldRight = new();
    
    public float Result
    {
        get => result;
        set
        {
            result = value;
            animator.SetFloat("Direction", result);
        }
    }

    public void TouchLeft()
    {
        onTouchLeft?.Invoke();
    }
    
    public void TouchRight()
    {
        onTouchRight?.Invoke();
    }

    public void AddIngredientLeft(IngredientWorld ingredient)
    {
        if (ingredientsWorldLeft.Count >= leftPositions.Count)
        {
            return;
        }
        
        //ToDo move ingredient to position leftPositions[ingredientsLeft.Count]
        
        ingredientsWorldLeft.Add(ingredient);
        CalcResult();
        onTouchLeft?.Invoke();
    }

    public void AddIngredientRight(IngredientWorld ingredient)
    {
        if (ingredientsWorldRight.Count >= rightPositions.Count)
        {
            return;
        }
        
        //ToDo move ingredient to position rightPositions[ingredientsRight.Count]
        
        ingredientsWorldRight.Add(ingredient);
        CalcResult();
        onTouchRight?.Invoke();
    }

    public void CalcResult()
    {
        var ingredientsLeft = new List<Ingredient>(ingredientsWorldLeft.Count);
        ingredientsWorldLeft.ForEach(ingredientWorld => ingredientsLeft.Add(ingredientWorld.ingredient));
        var ingredientsRight = new List<Ingredient>(ingredientsWorldRight.Count);
        ingredientsWorldRight.ForEach(ingredientWorld => ingredientsRight.Add(ingredientWorld.ingredient));

        var leftResult = ResolveWeight.CalculateWeight(ingredientsLeft);
        var rightResult = ResolveWeight.CalculateWeight(ingredientsRight);
        Result = rightResult - leftResult;
    }

    public void Reset()
    {
        //ToDo Reset Ingredients
        ingredientsWorldLeft.Clear();
        ingredientsWorldRight.Clear();
    }

    private void Awake()
    {
        onTouchLeft += OnTouchLeft;
        onTouchRight += OnTouchRight;
    }
    
    private void OnTouchRight()
    {
        animator.SetTrigger("TouchRight");
    }
    
    private void OnTouchLeft()
    {
        animator.SetTrigger("TouchLeft");
    }
}
