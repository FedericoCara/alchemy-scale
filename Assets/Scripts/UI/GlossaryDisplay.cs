using Model;
using UnityEngine;

namespace UI
{
    public class GlossaryDisplay : MonoBehaviour
    {
        public LevelManager levelManager;
        public GameObject glossaryButton;
        private void Start()
        {
            UpdateButton();
            levelManager.LevelNumberChanged += UpdateButton;
        }

        private void UpdateButton()
        {
            glossaryButton.SetActive(levelManager.LevelNumber>=2);
        }
    }
}
