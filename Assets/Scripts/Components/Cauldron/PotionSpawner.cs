using System;
using System.Collections;
using Model;
using UnityEngine;

namespace Components.Cauldron
{
    public class PotionSpawner : MonoBehaviour
    {
        public LevelManager levelManager;
        public Transform parent;
        public float delayUntilHide;

        public void SpawnPotion(Action onPotionShown)
        {
            var potion = Instantiate(levelManager.CurrentLevel.targetPotion, parent);
            StartCoroutine(HidePotion(potion, onPotionShown));
        }

        private IEnumerator HidePotion(GameObject potion, Action onPotionShown)
        {
            yield return new WaitForSeconds(delayUntilHide);
            potion.GetComponent<PotionAnimatorController>().Hide();
            onPotionShown?.Invoke();
        }
    }
}