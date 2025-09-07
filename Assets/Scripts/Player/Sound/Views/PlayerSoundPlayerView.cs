using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSoundPlayerView : MonoBehaviour
{
    private AudioSource _audioSource;

    private AudioSource AudioSource
    {
        get
        {
            if (_audioSource == null)
                _audioSource = GetComponent<AudioSource>();

            return _audioSource;
        }
    }

    public void PlayOneShot(AudioClip audioClip) => AudioSource.PlayOneShot(audioClip);
}
