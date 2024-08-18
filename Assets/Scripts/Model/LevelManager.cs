using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "AlchemyScale/Level Manager")]
    public class LevelManager : ScriptableObject
    {
        [SerializeField]
        private int currentLevelIndex;

        public int LevelNumber => currentLevelIndex + 1;
        public event Action LevelNumberChanged = () => { };
        
        [SerializeField]
        private List<Level> levels;


        public Level CurrentLevel => levels[currentLevelIndex];

        public Level GetAndSetNextLevel()
        {
            if (currentLevelIndex + 1 < levels.Count)
            {
                currentLevelIndex++;
                LevelNumberChanged.Invoke();
                return CurrentLevel;
            }
            return null;
        }

        private int _startingLevelIndex;

        private void OnEnable()
        {
            _startingLevelIndex = currentLevelIndex;
        }

        private void OnDisable()
        {
            currentLevelIndex = _startingLevelIndex;
        }
    }
}