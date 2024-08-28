using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "AlchemyScale/Level Manager")]
    public class LevelManager : ScriptableObject
    {
        public int currentLevelIndex;
        [SerializeField]
        private int _startingLevelIndex;

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
            else
            {
                currentLevelIndex = 0;
                LevelNumberChanged.Invoke();
                return null;
            }
        }


        private void OnEnable()
        {
            currentLevelIndex = _startingLevelIndex;
        }

        public void ResetGame()
        {
            currentLevelIndex = 0;
        }
    }
}