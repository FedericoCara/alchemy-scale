using UnityEngine;

namespace Utils
{
    [CreateAssetMenu(menuName = "AlchemyScale/ClampRect")]
    public class ClampRect : ScriptableObject
    {
        [SerializeField]
        private Rect rect;

        public Vector2 Clamp(Vector2 eventDataPosition)
        {
            return new Vector2(Mathf.Clamp(eventDataPosition.x, rect.xMin, rect.xMax),
                Mathf.Clamp(eventDataPosition.y,rect.yMin,rect.yMax));
        }
    }
}