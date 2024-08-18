using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Components.Cauldron
{
    public class CauldronPreview : MonoBehaviour
    {
        [SerializeField] private Transform previewParent;

        public void Add(Ingredient ingredient)
        {
            var icon = new GameObject(ingredient.name + "_cauldron_icon").AddComponent<Image>();
            Transform iconTransform = icon.transform;
            iconTransform.SetParent(previewParent);
            iconTransform.localPosition = Vector3.zero;
            iconTransform.localScale = Vector3.one;
            icon.sprite = ingredient.CauldronSprite;
        }

        public void Clear()
        {
            foreach (Transform ingredientIcon in previewParent)
            {
                Destroy(ingredientIcon.gameObject);
            }
        }
    }
}