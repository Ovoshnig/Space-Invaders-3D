using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

public class PlayerSoundPlayer : IInitializable, IDisposable
{
    private static readonly string[] _shootLabels = {
        PlayerSoundConstants.AudioLabel,
        PlayerSoundConstants.SFXLabel,
        PlayerSoundConstants.PlayerLabel,
        PlayerSoundConstants.ShootLabel,
    };
    private static readonly string[] _explosionLabels = {
        PlayerSoundConstants.AudioLabel,
        PlayerSoundConstants.SFXLabel,
        PlayerSoundConstants.PlayerLabel,
        PlayerSoundConstants.ExplosionLabel,
    };

    private readonly AddressableLoader _addressableLoader;
    private readonly PlayerShooter _playerShooter;
    private readonly PlayerDestroyer _playerDestroyer;
    private readonly Subject<AudioClip> _playing = new();
    private readonly CancellationTokenSource _cts = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    private AudioClip _shootClip;
    private AudioClip _explosionClip;

    public PlayerSoundPlayer(AddressableLoader addressableLoader,
        PlayerShooter playerShooter,
        PlayerDestroyer playerDestroyer)
    {
        _addressableLoader = addressableLoader;
        _playerShooter = playerShooter;
        _playerDestroyer = playerDestroyer;
    }

    public Observable<AudioClip> Playing => _playing;

    public void Initialize()
    {
        LoadClips().Forget();
        Subscribe();
    }

    public void Dispose()
    {
        _compositeDisposable.Dispose();

        _cts.Cancel();
        _cts.Dispose();

        _addressableLoader.ReleaseAssets(_shootLabels);
        _addressableLoader.ReleaseAssets(_explosionLabels);
    }

    private async UniTask LoadClips()
    {
        try
        {
            _shootClip = await LoadClipAsync(_shootLabels);
            _explosionClip = await LoadClipAsync(_explosionLabels);
        }
        catch (OperationCanceledException)
        {
        }
    }

    private async Task<AudioClip> LoadClipAsync(string[] labels) =>
        (await _addressableLoader.LoadAssets<AudioClip>(labels, _cts.Token)).FirstOrDefault();

    private void Subscribe()
    {
        _playerShooter.Shot
            .Subscribe(_ => _playing.OnNext(_shootClip))
            .AddTo(_compositeDisposable);

        _playerDestroyer.StartDestroying
            .Subscribe(_ => _playing.OnNext(_explosionClip))
            .AddTo(_compositeDisposable);
    }
}
