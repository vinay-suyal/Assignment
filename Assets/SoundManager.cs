using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip step;
    public AudioClip select;
    AudioSource audSrc;
    AudioSource main_audSrc;

    public static SoundManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        main_audSrc = transform.GetChild(0).GetComponent<AudioSource>();
        audSrc = transform.GetChild(1).GetComponent<AudioSource>();
    }

    public void PlayStepSound()
    {
        audSrc.PlayOneShot(step);
    }

    public void PlaySelectSound()
    {
        audSrc.clip = select;
        audSrc.Play();
    }
}
