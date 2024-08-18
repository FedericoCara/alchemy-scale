using System;
using UnityEngine;

namespace Components
{
    public class OnTriggerLevelBorder : MonoBehaviour
    {
        private int _layerLevelBorder;
        private IngredientsManager _manager;

        private void Start()
        {
            _manager = FindObjectOfType<IngredientsManager>();
            _layerLevelBorder = LayerMask.NameToLayer("LevelBorder");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == _layerLevelBorder &&
                _manager!=null)
            {
                transform.position = _manager.GetNextSpawnPoint().position;
                var rigidBody = transform.GetComponentInParent<Rigidbody>();
                if(rigidBody!=null)
                    rigidBody.velocity = Vector3.zero;
            }
        }
    }
}