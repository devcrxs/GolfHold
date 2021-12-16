using System.Collections;
using UnityEngine;
public class DeadPlayer : MonoBehaviour
{
    private float _waitSecondsTransition = 0.7f;
    [SerializeField] private Animator animatorPlayer;
    [SerializeField] private Rigidbody2D rbPlayer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (TagPlayer.IsPlayer(other))
        {
            StartCoroutine(WaitResetLevel());
        }
    }

    private IEnumerator WaitResetLevel()
    {
        AudioSourceManager.instance.PlayAudioDead();
        rbPlayer.bodyType = RigidbodyType2D.Static;
        AnimatorManager.AnimatorPlay(animatorPlayer,Constans.DEAD);
        yield return new WaitForSeconds(_waitSecondsTransition);
        UIManager.instance.RestartScene();
    }
}
