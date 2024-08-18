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
        
        [SerializeField]
        private List<Level> levels;

        public Level CurrentLevel => levels[currentLevelIndex];
    }
}