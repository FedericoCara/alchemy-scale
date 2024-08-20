using Model;
using UnityEngine;

namespace UI
{
    public class GlossaryDisplay : MonoBehaviour
    {
        public LevelManager levelManager;
        public GlossaryButtonController glossaryButton;
        public GameObject useMeCanvas;
        public int minLevelForGlossary = 2;
        private void Start()
        {
            UpdateButton();
            levelManager.LevelNumberChanged += UpdateButton;
        }

        private void UpdateButton()
        {
            glossaryButton.Interactable = levelManager.LevelNumber >= minLevelForGlossary;
            useMeCanvas.SetActive(levelManager.LevelNumber >= minLevelForGlossary);
        }
    }
}
