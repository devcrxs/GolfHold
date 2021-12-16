using UnityEngine;
public class SetPlayerPrefs : MonoBehaviour
{
    [SerializeField][Range(0,7)] private int actualLevel;
    void Start()
    {
        if (actualLevel > PlayerPrefs.GetInt(Constans.KEY_LEVEL))
        {
            PlayerPrefs.SetInt(Constans.KEY_LEVEL,actualLevel);
        }
    }
}
