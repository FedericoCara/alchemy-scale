using System;
using System.Collections.Generic;
using UnityEngine;

namespace Components.Cauldron
{
    public class CauldronButtonController : MonoBehaviour
    {
        [SerializeField]
        private List<Outline> outlines;

        [SerializeField]
        private CauldronWorld cauldronWorld;

        private bool _interactable = true;

        public bool Interactable
        {
            get => _interactable;
            set
            {
                _interactable = value;
                if(!value)
                    SetOutline(false);
            }
        }

        private Dictionary<Outline, float> outlineDefaultWidthValues = new();


        private void Awake()
        {
            foreach (Outline outline in outlines)
            {
                outlineDefaultWidthValues.Add(outline, outline.OutlineWidth);
                outline.OutlineWidth = 0;
            }
        }

        private void OnMouseEnter()
        {
            if(_interactable)
                SetOutline(true);
        }

        private void OnMouseExit()
        {
            SetOutline(false);
        }

        private void OnMouseUpAsButton()
        {
            if(_interactable)
                cauldronWorld.Mix();
        }

        private void SetOutline(bool outlineEnabled)
        {
            foreach (Outline outline in outlines)
            {
                outline.OutlineWidth = outlineEnabled ? outlineDefaultWidthValues[outline] :  0;
            }
        }
    }
}
