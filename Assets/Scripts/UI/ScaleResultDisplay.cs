using TMPro;
using UnityEngine;

public class ScaleResultDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private ScaleController scaleController;
    
    private void Start()
    {
        UpdateLevelNumber();
        scaleController.OnResultChanged += UpdateLevelNumber;
    }

    private void OnDestroy()
    {
        scaleController.OnResultChanged -= UpdateLevelNumber;
    }

    private void UpdateLevelNumber()
    {
        text.text = scaleController.Result.ToString();
    }
}
