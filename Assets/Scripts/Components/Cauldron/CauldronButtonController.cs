using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components.Cauldron
{
    public class CauldronButtonController : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip clip;

        [SerializeField]
        private List<Outline> outlines;

        [SerializeField]
        private CauldronWorld cauldronWorld;

        [SerializeField]
        private float defaultWidth = 4;

        private bool _interactable = false;

        public bool Interactable
        {
            get => _interactable;
            set
            {
                _interactable = value;

                if(!value)
                {
                    SetOutline(false);
                }

                Debug.Log($"Me setearon {_interactable}");
            }
        }


        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();

            foreach (Outline outline in outlines)
            {
                outline.OutlineWidth = 0;
            }
        }

        private void OnMouseOver()
        {
            if (_interactable)
            {
                SetOutline(true);
            }
        }

        private void OnMouseExit()
        {
            SetOutline(false);
        }

        private void OnMouseUpAsButton()
        {
            if (_interactable)
            {
                audioSource.PlayOneShot(clip);
                cauldronWorld.Mix();
            }
        }

        private void SetOutline(bool outlineEnabled)
        {
            foreach (Outline outline in outlines)
            {
                outline.OutlineWidth = outlineEnabled ? defaultWidth :  0;
            }
        }
    }
}
