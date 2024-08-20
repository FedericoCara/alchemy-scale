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
    [SerializeField] private GameObject resetScaleButton;

    private event Action onTouchLeft;
    private event Action onTouchRight;
    private float result;

    private List<IngredientWorld> ingredientsWorldLeft = new();
    private List<IngredientWorld> ingredientsWorldRight = new();

    private Draggable currentDraggable;
    private IngredientsManager _spawner;

    private float debounceTime = 0.5f; // Time in seconds to wait before allowing another addition
    private float lastDropTime = 0f;

    public float Result
    {
        get => result;
        set
        {
            result = value;
            animator.SetFloat("Direction", result);
            OnResultChanged?.Invoke();
        }
    }
    
    public event Action OnResultChanged;

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
    
    public void RemoveLastItemLeft()
    {
        if (ingredientsWorldLeft.Count > 0)
        {
            var lastItem = ingredientsWorldLeft[ingredientsWorldLeft.Count - 1];
            RemoveItem(lastItem, leftPositions);
            ingredientsWorldLeft.RemoveAt(ingredientsWorldLeft.Count - 1);
            CalcResult();
            EnableResetButton(true);
        }
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

    public void RemoveLastItemRight()
    {
        if (ingredientsWorldRight.Count > 0)
        {
            var lastItem = ingredientsWorldRight[ingredientsWorldRight.Count - 1];
            RemoveItem(lastItem, rightPositions);
            ingredientsWorldRight.RemoveAt(ingredientsWorldRight.Count - 1);
            CalcResult();
            EnableResetButton(true);
        }
    }

    private void RemoveItem(IngredientWorld ingredientWorld, List<Transform> positions)
    {
        // Reset the item's position to be outside the scale
        ingredientWorld.transform.position = new Vector3(ingredientWorld.transform.position.x, ingredientWorld.transform.position.y, _spawner.GetNextSpawnPoint().position.z);

        var rigidBody = ingredientWorld.transform.GetComponentInParent<Rigidbody>();

        if (rigidBody != null)
        {
            rigidBody.isKinematic = false;
            rigidBody.velocity = Vector3.zero;
        }

        ingredientWorld.transform.parent = null;

        var draggable = ingredientWorld.transform.GetComponentInParent<Draggable>();

        if (draggable != null)
        {
            draggable.Interactable = true;
            draggable.OnDragEndListener -= OnDropIngredient;
        }
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
        EnableResetButton(false);
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
        EnableResetButton(false);
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
        if (draggable != null && draggable.isDragging)
        {
            currentDraggable = draggable;
            currentDraggable.OnDragEndListener += OnDropIngredient;
        }
    }

    private void OnDropIngredient(Draggable draggable)
    {
        if (Time.time - lastDropTime < debounceTime)
        {
            return; // Skip if the debounce time hasn't elapsed
        }

        lastDropTime = Time.time;

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
        EnableResetButton(true);
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

    private void EnableResetButton(bool enabled)
    {
        resetScaleButton.SetActive(enabled);
    }
}
