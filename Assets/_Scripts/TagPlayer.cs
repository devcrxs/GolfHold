using UnityEngine;
public class TagPlayer : MonoBehaviour
{
    public static bool IsPlayer(Collider2D other)
    {
        return other.CompareTag(Constans.TAG_PLAYER);
    }
}
