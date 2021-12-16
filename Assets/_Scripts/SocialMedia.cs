using UnityEngine;
public class SocialMedia : MonoBehaviour
{
    public void GoUrl(string url)
    {
        AudioSourceManager.instance.PlayAudioButton();
        Application.OpenURL(url);
    }
}
