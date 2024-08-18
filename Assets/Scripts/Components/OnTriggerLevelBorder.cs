using System;
using UnityEngine;

namespace Components
{
    public class OnTriggerLevelBorder : MonoBehaviour
    {
        private int _layerLevelBorder;
        private IngredientsSpawner _spawner;

        private void Start()
        {
            _spawner = FindObjectOfType<IngredientsSpawner>();
            _layerLevelBorder = LayerMask.NameToLayer("LevelBorder");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == _layerLevelBorder &&
                _spawner!=null)
            {
                transform.position = _spawner.GetNextSpawnPoint().position;
            }
        }
    }
}