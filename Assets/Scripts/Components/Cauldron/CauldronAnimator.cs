using System;
using Meshes.Enviroment;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components.Cauldron
{
    public class CauldronAnimator : MonoBehaviour
    {
        public CauldronSpinWater cauldronSpinWater;

        public void AnimateSpin()
        {
            cauldronSpinWater.Spin();
            Invoke(nameof(StopSpinning),3);
        }

        public void StopSpinning()
        {
            cauldronSpinWater.StopSpinning();
        }
    }
}