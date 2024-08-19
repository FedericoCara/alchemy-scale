using System;
using DG.Tweening;
using Meshes.Enviroment;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components.Cauldron
{
    public class CauldronAnimator : MonoBehaviour
    {
        [SerializeField] private CauldronSpinWater cauldronSpinWater;
        [SerializeField] private float mixTime = 3;
        [SerializeField] private MeshRenderer waterRenderer;
        [SerializeField] private float blackWaterDelay = 2;
        [SerializeField] private float blackWaterDuration = 1;
        [SerializeField] private Color blackWaterColor = Color.black;
        [SerializeField] private Color successWaterColor = Color.green;
        [SerializeField] private Color clearWaterColor = Color.white;
        [SerializeField] private ParticleSystem boilingBubbles;
        private Material waterMaterial;
        private Action _onCauldronFinishedSpinning;
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

        private void Awake()
        {
            waterMaterial = waterRenderer.sharedMaterial;
        }

        public void AnimateSpin(bool success, Action onCauldronFinishedSpinning)
        {
            cauldronSpinWater.Spin();
            _onCauldronFinishedSpinning = onCauldronFinishedSpinning;
            DOTween.To(GetWaterColor,SetWaterColor,success ? successWaterColor : blackWaterColor, blackWaterDuration)
                .SetDelay(blackWaterDelay)
                .OnStart(CreateBigBubbles)
                .OnComplete(MakeClearColor);
            Invoke(nameof(StopSpinning),blackWaterDuration*2+blackWaterDelay+mixTime);
        }

        private void CreateBigBubbles()
        {
            boilingBubbles.Play();
        }
        
        private void HideBigBubbles()
        {
            boilingBubbles.Stop();
        }


        private Color GetWaterColor()
        {
            return waterMaterial.color;
        }

        private void SetWaterColor(Color pnewvalue)
        {
            waterMaterial.color = pnewvalue;
            waterMaterial.SetColor(BaseColor, pnewvalue);
            waterMaterial.SetColor(EmissionColor, pnewvalue);
        }

        private void MakeClearColor()
        {
            DOTween.To(GetWaterColor, SetWaterColor, clearWaterColor, blackWaterDuration)
                .SetDelay(mixTime);
        }

        public void StopSpinning()
        {
            HideBigBubbles();
            cauldronSpinWater.StopSpinning();
            _onCauldronFinishedSpinning?.Invoke();
        }
    }
}