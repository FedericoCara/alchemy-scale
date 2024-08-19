using UnityEngine;

namespace Components
{
    public class PotionAnimatorController : MonoBehaviour
    {
        public Animator animator;
        public float destroyDelayAfterHide = 2;
        private static readonly int HideId = Animator.StringToHash("Hide");

        public void Hide()
        {
            animator.SetTrigger(HideId);
            Destroy(gameObject,destroyDelayAfterHide);
        }
    }
}