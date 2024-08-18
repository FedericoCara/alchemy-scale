using System;
using System.Collections.Generic;
using Components;
using Model;
using Unity.VisualScripting;
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

    private Draggable currentDraggable;
    private IngredientsManager _spawner;
    
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

        ingredient.transform.position = leftPositions[ingredientsWorldLeft.Count].position;
        ingredient.transform.parent = leftPositions[ingredientsWorldLeft.Count];
        ingredient.GetComponent<Rigidbody>().isKinematic = true;

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
        
        ingredient.transform.position = rightPositions[ingredientsWorldRight.Count].position;
        ingredient.transform.parent = rightPositions[ingredientsWorldRight.Count];
        ingredient.GetComponent<Rigidbody>().isKinematic = true;
            
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
        foreach (var ingredientWorld in ingredientsWorldLeft)
        {
            ResetIngredientPosition(ingredientWorld);
        }
        
        foreach (var ingredientWorld in ingredientsWorldRight)
        {
            ResetIngredientPosition(ingredientWorld);
        }
        
        ingredientsWorldLeft.Clear();
        ingredientsWorldRight.Clear();
        
        CalcResult();
    }
    private void ResetIngredientPosition(IngredientWorld ingredientWorld)
    {
        ingredientWorld.transform.position = _spawner.GetNextSpawnPoint().position;
        var rigidBody = ingredientWorld.transform.GetComponentInParent<Rigidbody>();
        if (rigidBody != null)
        {
            rigidBody.isKinematic = false;
            rigidBody.velocity = Vector3.zero;
        }
        ingredientWorld.transform.parent = null;
        var draggable = ingredientWorld.transform.GetComponentInParent<Draggable>();
        draggable.Interactable = true;
    }

    private void Awake()
    {
        onTouchLeft += OnTouchLeft;
        onTouchRight += OnTouchRight;
    }
    
    private void Start()
    {
        _spawner = FindObjectOfType<IngredientsManager>(); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    private void OnTouchRight()
    {
        animator.SetTrigger("TouchRight");
    }
    
    private void OnTouchLeft()
    {
        animator.SetTrigger("TouchLeft");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent == null)
        {
            return;
        }
        
        var parentGameObject = other.gameObject.transform.parent.gameObject;
        var draggable = parentGameObject.GetComponent<Draggable>();
        if (draggable.isDragging)
        {
            currentDraggable = draggable;
            currentDraggable.OnDragEndListener += OnDropIngredient;
        }
    }
    
    private void OnDropIngredient(Draggable draggable)
    {
        var ingredientWorld = draggable.gameObject.GetComponent<IngredientWorld>();
        if (IsOnLeft(ingredientWorld))
        {
            AddIngredientLeft(ingredientWorld);
        }
        else
        {
            AddIngredientRight(ingredientWorld);
        }
        draggable.Interactable = false;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.parent == null)
        {
            return;
        }
        
        var parentGameObject = other.gameObject.transform.parent.gameObject;
        var draggable = parentGameObject.GetComponent<Draggable>();
        if (draggable == currentDraggable)
        {
            currentDraggable.OnDragEndListener -= OnDropIngredient;
            currentDraggable = null;
        }
    }

    private bool IsOnLeft(IngredientWorld ingredientWorld)
    {
        return ingredientWorld.transform.position.x > transform.position.x; // rotated 180
    }
}
