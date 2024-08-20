using Components.Cauldron;
using UnityEngine;

namespace Components
{
    public class CameraAnimator : MonoBehaviour
    {
        public GameManager gameManager;
        public Animator animator;
        public CauldronWorld cauldronWorld;
        private static readonly int Mix = Animator.StringToHash("Mix");
        private static readonly int Main = Animator.StringToHash("Main");
        private static readonly int Table = Animator.StringToHash("Table");

        public void MoveToMixPosition()
        {
            animator.SetTrigger(Mix);
        }

        public void ReturnToMainPosition()
        {
            animator.SetTrigger(Main);
        }

        public void OnMixPositionReached()
        {
            cauldronWorld.OnAnimateStartFinished();
        }

        public void MoveToTutorialPosition()
        {
            animator.SetTrigger(Table);
        }

        public void OnReturnFromTutorialFinished()
        {
            gameManager.OnTutorialFinished();
        }
    }
}