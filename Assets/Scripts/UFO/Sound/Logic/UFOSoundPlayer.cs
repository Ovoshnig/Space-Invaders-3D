using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

public class UFOSoundPlayer : IInitializable, IDisposable
{
    private static readonly string[] _movementLabels = {
        UFOSoundConstants.AudioLabel,
        UFOSoundConstants.SFXLabel,
        UFOSoundConstants.UFOLabel,
        UFOSoundConstants.MovementLabel,
    };
    private static readonly string[] _explosionLabels = {
        UFOSoundConstants.AudioLabel,
        UFOSoundConstants.SFXLabel,
        UFOSoundConstants.InvaderLabel,
        UFOSoundConstants.ExplosionLabel,
    };

    private readonly AddressableLoader _addressableLoader;
    private readonly UFOMover _ufoMover;
    private readonly UFODestroyer _ufoDestroyer;
    private readonly Subject<AudioClip> _playing = new();
    private readonly CancellationTokenSource _cts = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    private AudioClip _movementClip;
    private AudioClip _explosionClip;
    private float _previousMovePlayingTime = 0f;

    public UFOSoundPlayer(AddressableLoader addressableLoader,
        UFOMover ufoMover,
        UFODestroyer ufoDestroyer)
    {
        _addressableLoader = addressableLoader;
        _ufoMover = ufoMover;
        _ufoDestroyer = ufoDestroyer;
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

        _addressableLoader.ReleaseAssets(_movementLabels);
        _addressableLoader.ReleaseAssets(_explosionLabels);
    }

    private async UniTask LoadClips()
    {
        try
        {
            _movementClip = await LoadClipAsync(_movementLabels, 1);
            _explosionClip = await LoadClipAsync(_explosionLabels, 0);
        }
        catch (OperationCanceledException)
        {
        }
    }

    private async Task<AudioClip> LoadClipAsync(string[] labels, int index) =>
        (await _addressableLoader.LoadAssets<AudioClip>(labels, _cts.Token)).ElementAtOrDefault(index);

    private void Subscribe()
    {
        _ufoMover.Moved
            .Subscribe(_ =>
            {
                if (Time.time - _previousMovePlayingTime > _movementClip.length)
                {
                    _playing.OnNext(_movementClip);
                    _previousMovePlayingTime = Time.time;
                }
            })
            .AddTo(_compositeDisposable);

        _ufoDestroyer.Destroyed
            .Subscribe(_ => _playing.OnNext(_explosionClip))
            .AddTo(_compositeDisposable);
    }
}
