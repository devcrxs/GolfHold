using UnityEngine;
public class AnimatorManager : MonoBehaviour
{
    public static void AnimatorPlay(Animator animator, string parametre)
    {
        animator.Play(parametre,0);
    }

    public static void AnimatorSetBool(Animator animator, string parametre, bool value)
    {
        animator.SetBool(parametre,value);
    }
}
