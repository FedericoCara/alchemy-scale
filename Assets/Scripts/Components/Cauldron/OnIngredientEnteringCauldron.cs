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

        private IngredientsManager _spawner;

        private void Start()
        {
            _spawner = FindObjectOfType<IngredientsManager>();
        }

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

                if (ingredient.ingredient.name != "Weight_01" && ingredient.ingredient.name != "Weight_02")
                {
                    cauldron.AddIngredient(ingredient);
                }
                else
                {
                    ResetIngredientPosition(ingredient);
                    
                }
            }
        }

        private void ResetIngredientPosition(IngredientWorld ingredientWorld)
        {
            ingredientWorld.transform.position = _spawner.GetNextSpawnPoint().position;
            var rigidBody = ingredientWorld.transform.GetComponentInParent<Rigidbody>();
            if (rigidBody != null)
            {
                rigidBody.isKinematic = false;
                rigidBody.velocity = Vector3.zero;
            }
            ingredientWorld.transform.parent = null;
            var draggable = ingredientWorld.transform.GetComponentInParent<Draggable>();
            draggable.Interactable = true;
            draggable.StopDragging();
        }
    }
}
