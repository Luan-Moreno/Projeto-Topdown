using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip bgmMusic;
    private AudioManager audioM;
    void Start()
    {
        audioM = FindAnyObjectByType<AudioManager>();
        
        audioM.PlayBGM(bgmMusic);
    }
}
