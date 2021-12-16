using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private int _waitSecondsTransition = 1;
    [SerializeField] private Text textAttempts;
    [SerializeField] private Animator animatorTransition;
    [SerializeField] private Animator animatorWin;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void NextLevel(string nameScene)
    {
        StartCoroutine(WaitLoadNextLevel(nameScene));
    }

    public void GoScene(string nameScene)
    {
        StartCoroutine(WaitLoadScene(nameScene));
    }

    public void RestartScene()
    {
        StartCoroutine(WaitLoadScene(SceneManager.GetActiveScene().name));
    }

    private IEnumerator WaitLoadScene(string nameScene)
    {
        AudioSourceManager.instance.PlayAudioButton();
        AudioSourceManager.instance.ReduceSound();
        AnimatorManager.AnimatorPlay(animatorTransition, Constans.OUT);
        yield return new WaitForSeconds(_waitSecondsTransition);
        SceneManager.LoadSceneAsync(nameScene);
    }
    
    private IEnumerator WaitLoadNextLevel(string nameScene)
    {
        AudioSourceManager.instance.PlayAudioButton();
        AudioSourceManager.instance.ReduceSound();
        AnimatorManager.AnimatorPlay(animatorWin, Constans.OUT);
        yield return new WaitForSeconds(_waitSecondsTransition);
        SceneManager.LoadSceneAsync(nameScene);
    }

    private void LateUpdate()
    {
        ShowTextAttempts();
    }

    private void ShowTextAttempts()
    {
        textAttempts.text = GameManager.instance.availableAttempts.ToString();
    }
}
