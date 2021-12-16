using UnityEngine;
using DG.Tweening;
public class TutorialManager : MonoBehaviour
{
    private float offsetY = 2;
    private float offsetX = 3.5f;
    private float timeMove = 1f;
    private Vector2 startPosTutorial;
    [SerializeField] private GameObject touchTutorial;
    [SerializeField] private GameObject trailTouch;
    [SerializeField] private GameObject canvasTutorial;

    private void Start()
    {
        DOTween.Init();
        startPosTutorial = touchTutorial.transform.position;
        canvasTutorial.SetActive(true);
        touchTutorial.SetActive(false);
    }

    public void ContinueTutorial()
    {
        AudioSourceManager.instance.PlayAudioButton();
        canvasTutorial.SetActive(false);
        touchTutorial.SetActive(true);
        DownTouch();
    }

    private void DownTouch()
    {
        if (!GameManager.instance.isGameOver)
        {
            trailTouch.SetActive(true);
            var newPosDown = startPosTutorial;
            newPosDown.x -= offsetX;
            newPosDown.y -= offsetY;
            touchTutorial.transform.DOLocalMove(newPosDown, timeMove).OnComplete(UpTouch);
        }
        
    }

    private void UpTouch()
    {
        if (!GameManager.instance.isGameOver)
        {
            trailTouch.SetActive(false);
            touchTutorial.transform.DOLocalMove(startPosTutorial, timeMove).OnComplete(DownTouch);
        }
    }
}
