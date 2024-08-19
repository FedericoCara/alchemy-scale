using System;
using Model;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Components.Cauldron
{
    public class CauldronPreview : MonoBehaviour
    {
        [SerializeField] private Transform previewParent;
        [SerializeField] private float preferredSize = 20;

        public void Add(Ingredient ingredient)
        {
            var icon = new GameObject(ingredient.name + "_cauldron_icon").AddComponent<Image>();
            RectTransform iconTransform = icon.rectTransform;
            iconTransform.SetParent(previewParent);
            iconTransform.localPosition = Vector3.zero;
            iconTransform.localScale = Vector3.one;
            iconTransform.sizeDelta = Vector2.one * preferredSize;
            icon.sprite = ingredient.CauldronSprite;
            icon.preserveAspect = true;
            icon.AddComponent<LayoutElement>().preferredWidth = preferredSize;
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