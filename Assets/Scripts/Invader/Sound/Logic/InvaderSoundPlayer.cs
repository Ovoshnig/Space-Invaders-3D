using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

public class InvaderSoundPlayer : IInitializable, IDisposable
{
    private static readonly string[] _movementLabels = {
        InvaderSoundConstants.AudioLabel,
        InvaderSoundConstants.SFXLabel,
        InvaderSoundConstants.InvaderLabel,
        InvaderSoundConstants.MovementLabel,
    };
    private static readonly string[] _explosionLabels = {
        InvaderSoundConstants.AudioLabel,
        InvaderSoundConstants.SFXLabel,
        InvaderSoundConstants.InvaderLabel,
        InvaderSoundConstants.ExplosionLabel,
    };

    private readonly AddressableLoader _addressableLoader;
    private readonly InvaderMover _invaderMover;
    private readonly CollidedDestroyer<InvaderEntityView, PlayerBulletMoverView> _invaderDestroyer;
    private readonly Subject<AudioClip> _playing = new();
    private readonly CancellationTokenSource _cts = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    private AudioClip _movementClip;
    private AudioClip _explosionClip;

    public InvaderSoundPlayer(AddressableLoader addressableLoader,
        InvaderMover invaderMover,
        CollidedDestroyer<InvaderEntityView, PlayerBulletMoverView> invaderDestroyer)
    {
        _addressableLoader = addressableLoader;
        _invaderMover = invaderMover;
        _invaderDestroyer = invaderDestroyer;
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
            _movementClip = await LoadClipAsync(_movementLabels);
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
        _invaderMover.Moved
            .Subscribe(_ => _playing.OnNext(_movementClip))
            .AddTo(_compositeDisposable);

        _invaderDestroyer.Destroyed
            .Subscribe(_ => _playing.OnNext(_explosionClip))
            .AddTo(_compositeDisposable);
    }
}
