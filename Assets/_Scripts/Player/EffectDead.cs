using UnityEngine;
public class EffectDead : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystemSmoke;
    public void SmokeDead()
    {
        particleSystemSmoke.Play();
    }
}
