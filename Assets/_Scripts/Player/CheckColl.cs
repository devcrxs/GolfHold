using UnityEngine;
public class CheckColl : MonoBehaviour
{
    private bool _canDrag = true;
    private bool effectsGround;

    [SerializeField] private Vector2 offset;
    [SerializeField] private float collisionRadius;
    [SerializeField] private ParticleSystem particleSystemDust;
    public LayerMask groundLayer;
    public bool CanDrag => _canDrag;

    private void Update()
    {
       CheckGround();
       EffectsTouchGround();
    }
    private void CheckGround()
    {
        var positionCircle = (Vector2) transform.position + offset;
        _canDrag = Physics2D.OverlapCircle(positionCircle, collisionRadius, groundLayer);
    }

    private void EffectsTouchGround()
    {
        if (_canDrag)
        {
            if (effectsGround)
            {
                effectsGround = false;
                AudioSourceManager.instance.PlayAudioTouchGround();
                particleSystemDust.Play();
            }
            
        }
        else
        {
            effectsGround = true;
        }
    }
}
