using Model;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PotionDisplay : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private LevelManager levelManager;
    
        private void Start()
        {
            UpdateLevelPotion();
            levelManager.LevelNumberChanged += UpdateLevelPotion;
        }

        private void UpdateLevelPotion()
        {
            image.sprite = levelManager.CurrentLevel.targetPotionIcon;
            image.SetNativeSize();
        }
    }
}
