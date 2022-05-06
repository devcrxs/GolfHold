using UnityEngine;
using UnityEngine.EventSystems;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameOver;
    public int availableAttempts;
    [SerializeField] private Rigidbody2D rb2DPlayer;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Update()
    {
        ComprobationAttempts();
    }

    private void ComprobationAttempts()
    {
        if (IsAttemptsCompleted())
        {
            if (IsStaticPlayer())
            {
                isGameOver = true;
                UIManager.instance.RestartScene();
            }
        }
    }

    private bool IsAttemptsCompleted()
    {
        return availableAttempts <= Constans.ZERO && !isGameOver;
    }

    private bool IsStaticPlayer()
    {
        if (Input.touchCount > Constans.ZERO && !HoyoControl.instance.IsNext)
        {
            Touch touch = Input.GetTouch(Constans.ZERO);
            return !EventSystem.current.IsPointerOverGameObject(touch.fingerId) && rb2DPlayer.velocity.magnitude == 0;
        }
        return false;
    }
}
