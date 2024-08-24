using Components.Ingredients;
using Model;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components.Cauldron
{
    public class OnIngredientEnteringCauldron : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip clip_01;
        public AudioClip clip_02;

        [SerializeField] private CauldronWorld cauldron;
        private void OnTriggerEnter(Collider other)
        {
            var ingredient = other.GetComponentInParent<IngredientWorld>();
            if (ingredient != null)
            {
                int randomSound = Random.Range(0, 2);

                if (randomSound == 0)
                {
                    audioSource.PlayOneShot(clip_01);
                }
                else
                {
                    audioSource.PlayOneShot(clip_02);
                }

                cauldron.AddIngredient(ingredient);
            }
        }
    }
}
