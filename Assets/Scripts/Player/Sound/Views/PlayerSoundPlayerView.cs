using UnityEngine;
using UnityEngine.AddressableAssets;

[RequireComponent(typeof(AudioSource))]
public class PlayerSoundPlayerView : MonoBehaviour
{
    [SerializeField] private AssetReference _shootReference;
    [SerializeField] private AssetReference _deathReference;

    private AudioSource _audioSource;
    private AudioClip _shootResource;
    private AudioClip _deathResource;

    public AssetReference ShootReference => _shootReference;
    public AssetReference DeathReference => _deathReference;

    private AudioSource AudioSource
    {
        get
        {
            if (_audioSource == null)
                _audioSource = GetComponent<AudioSource>();

            return _audioSource;
        }
    }

    public void SetResources(AudioClip shootResource, AudioClip deathResource)
    {
        _shootResource = shootResource;
        _deathResource = deathResource;
    }

    public void PlayShootSound() => AudioSource.PlayOneShot(_shootResource);

    public void PlayDeathSound() => AudioSource.PlayOneShot(_deathResource);
}
