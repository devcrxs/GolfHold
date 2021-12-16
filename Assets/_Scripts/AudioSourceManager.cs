using UnityEngine;
public class AudioSourceManager : MonoBehaviour
{
    private float minVolume = 0.1f;
    private float maxVolume = 0.5f;
    private float volumeInitial = 0f;
    private float lerpTime = 0.001f;
    public static AudioSourceManager instance;
    [SerializeField] private AudioSource audioSource;
    [Header("AudioClips")] [Space(3)]
    [SerializeField] private AudioClip audioClipShootBall;
    [SerializeField] private AudioClip audioClipDead;
    [SerializeField] private AudioClip audioClipDiamond;
    [SerializeField] private AudioClip audioClipHoyo;
    [SerializeField] private AudioClip audioClipButton;
    [SerializeField] private AudioClip audioClipTouchGround;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        IncreaseSound();
    }

    public void ReduceSound()
    {
        while (audioSource.volume > minVolume)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, Constans.ZERO,Time.deltaTime);
        }
    }
    
    private void IncreaseSound()
    {
        audioSource.volume = volumeInitial;
        for (float i = 0; i <= maxVolume; i +=lerpTime)
        {
            audioSource.volume = i;
        }
    }

    public void PlayAudioShoot()
    {
        audioSource.PlayOneShot(audioClipShootBall);
    }

    public void PlayAudioDead()
    {
        audioSource.PlayOneShot(audioClipDead);
    }

    public void PlayAudioDiamond()
    {
        audioSource.PlayOneShot(audioClipDiamond);
    }

    public void PlayAudioHoyo()
    {
        audioSource.PlayOneShot(audioClipHoyo);
    }

    public void PlayAudioButton()
    {
        audioSource.PlayOneShot(audioClipButton);
    }

    public void PlayAudioTouchGround()
    {
        audioSource.PlayOneShot(audioClipTouchGround);
    }
}
