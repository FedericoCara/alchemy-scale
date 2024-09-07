using UnityEngine;

namespace Meshes.Environment.Cauldron
{
    public class CauldronSpinWater : MonoBehaviour
    {
        [SerializeField] private float spinMaxSpeed = 30;
        private float spinSpeed = 0;
        private bool spinning;

        void Update()
        {
            spinSpeed = Mathf.Lerp(spinSpeed, spinning ? spinMaxSpeed : 0, Time.deltaTime);
            transform.eulerAngles += Vector3.up*spinSpeed*Time.deltaTime;
        }

        public void Spin()
        {
            spinning = true;
        }

        public void StopSpinning()
        {
            spinning = false;
        }
    }
}
