using UnityEngine;
public class LockLevels : MonoBehaviour
{
    private int _defaultLevel = 0;
    [SerializeField] private GameObject[] locks;
    private void Start()
    {
        CheckLevelsLock();
    }

    private void CheckLevelsLock()
    {
        for (int i = 0; i <= PlayerPrefs.GetInt(Constans.KEY_LEVEL); i++)
        {
            IsActiveGameObject(locks[i],false);
        }
        IsActiveGameObject(locks[_defaultLevel],false);
    }

    private void IsActiveGameObject(GameObject go, bool value)
    {
        go.SetActive(value);
    }
}
