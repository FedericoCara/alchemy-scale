using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Components
{
    public class IngredientsManager : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private List<Transform> spawnPoints;
        [SerializeField] private Transform spawnParent;
        [SerializeField] private List<IngredientWorld> ingredientPrefabs;
        [SerializeField] private float timeBetweenSpawns = 0.2f;
        [SerializeField] private float randomTimeBetweenSpawns = 0.05f;
        [SerializeField] private float previewHeight = 10;
        private List<Transform> remainingSpawnPoints = new();
        private Dictionary<Ingredient, IngredientWorld> _ingredientDictionary;
        private List<IngredientWorld> _ingredientsSpawned = new();

        private void Awake()
        {
            BuildIngredientDictionary();
        }

        private void Start()
        {
            RestartIngredients();
        }

        public void RestartIngredients()
        {
            StartCoroutine(RestartIngredientCoroutine());
        }

        public void ClearIngredients()
        {
            foreach (var ingredientWorld in _ingredientsSpawned)
            {
                Destroy(ingredientWorld.gameObject);
            }
            _ingredientsSpawned.Clear();
        }

        private IEnumerator RestartIngredientCoroutine()
        {
            foreach (var startingIngredient in levelManager.CurrentLevel.startingIngredients)
            {
                for(var i = 0; i<startingIngredient.amount;i++)
                {
                    var nextSpawnPoint = GetNextSpawnPoint();
                    _ingredientsSpawned.Add(Instantiate(_ingredientDictionary[startingIngredient.ingredient],nextSpawnPoint.position,Quaternion.Euler(0,0,Random.value*10), spawnParent));
                    yield return new WaitForSeconds(timeBetweenSpawns+Random.value*randomTimeBetweenSpawns);
                }
            }
        }

        public Transform GetNextSpawnPoint()
        {
            if(remainingSpawnPoints.Count<=0)
                remainingSpawnPoints.AddRange(spawnPoints);
            var nextSpawnPointIndex = Random.Range(0, remainingSpawnPoints.Count);
            var nextSpawnPoint = spawnPoints[nextSpawnPointIndex];
            remainingSpawnPoints.RemoveAt(nextSpawnPointIndex);
            return nextSpawnPoint;
        }

        private void BuildIngredientDictionary()
        {
            _ingredientDictionary = new Dictionary<Ingredient, IngredientWorld>(ingredientPrefabs.Count);
            foreach (var ingredientPrefab in ingredientPrefabs)
            {
                _ingredientDictionary.Add(ingredientPrefab.ingredient,ingredientPrefab);
            }
        }

        private void OnDrawGizmosSelected()
        {
            var position = transform.position;
            var minX = spawnPoints.Min(spawnPoint => spawnPoint.position.x);
            var maxX = spawnPoints.Max(spawnPoint => spawnPoint.position.x);
            var centerX = minX + (maxX - minX)/2;
            Gizmos.DrawWireCube(
                new Vector3(centerX,position.y-previewHeight/2,position.z),
                new Vector3(maxX - minX,previewHeight,0));
        }
    }
}
