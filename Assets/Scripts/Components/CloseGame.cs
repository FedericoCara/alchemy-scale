using UnityEngine;

namespace Components
{
    public class CloseGame : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.ExitPlaymode();
                #else
                Application.Quit();
                #endif
            }
        }
    }
}
