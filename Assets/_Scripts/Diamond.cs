using Unity.Mathematics;
using UnityEngine;
public class Diamond : MonoBehaviour
{
    [SerializeField] private GameObject effectDiamond;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (TagPlayer.IsPlayer(other))
        {
            Instantiate(effectDiamond, transform.position, quaternion.identity);
            AudioSourceManager.instance.PlayAudioDiamond();
            HoyoControl.instance.DiamondCurrent++;
            Destroy(gameObject);
        }
    }
}
