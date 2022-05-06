using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;
    private int _waitSecondsTransition = 1;
    private float timeMoveCanvas = 0.4f;
    private float positionHideCanvas = 1400;
    [SerializeField] private Animator animatorTransition;
    [SerializeField] private Transform canvasSelectLevel;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        DOTween.Init();
    }

    public void GoScene(string nameScene)
    {
        StartCoroutine(WaitLoadScene(nameScene));
    }

    private IEnumerator WaitLoadScene(string nameScene)
    {
        AudioSourceManager.instance.PlayAudioButton();
        AudioSourceManager.instance.ReduceSound();
        AnimatorManager.AnimatorPlay(animatorTransition, Constans.OUT);
        yield return new WaitForSeconds(_waitSecondsTransition);
        SceneManager.LoadSceneAsync(nameScene);
    }

    public void ShowSelectLevel()
    {
        AudioSourceManager.instance.PlayAudioButton();
        canvasSelectLevel.DOLocalMoveX(Constans.ZERO,timeMoveCanvas);
    }

    public void HideSelectLevel()
    {
        AudioSourceManager.instance.PlayAudioButton();
        canvasSelectLevel.DOLocalMoveX(-positionHideCanvas, timeMoveCanvas).OnComplete(DefaultPositionCanvas);
    }

    private void DefaultPositionCanvas()
    {
        canvasSelectLevel.DOLocalMoveX(positionHideCanvas, Constans.ZERO);
    }
    
    public void GoUrl(string url)
    {
        AudioSourceManager.instance.PlayAudioButton();
        Application.OpenURL(url);
    }
    
}
