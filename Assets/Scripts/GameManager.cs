using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject successGO;
    [SerializeField] private GameObject failGO;

    public void SetSuccess()
    {
        successGO.SetActive(true);
    }

    public void SetFail()
    {
        failGO.SetActive(true);
    }
}
