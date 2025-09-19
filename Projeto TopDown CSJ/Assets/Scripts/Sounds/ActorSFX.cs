using UnityEngine;

public class ActorSFX : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void PlaySFX(AudioClip clip)
    {
        audioSource.volume = 0.1f;
        audioSource.PlayOneShot(clip);
    }
}
