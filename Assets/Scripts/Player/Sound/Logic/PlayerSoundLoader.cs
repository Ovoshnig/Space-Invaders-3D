using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayerSoundLoader
{
    private AudioClip _shootResource;
    private AudioClip _deathResource;
    private AsyncOperationHandle<AudioClip> _shootHandle;
    private AsyncOperationHandle<AudioClip> _deathHandle;
    private readonly CancellationTokenSource _cts = new();

    public async UniTask<(AudioClip shootResource, AudioClip deathResource)> LoadSoundsAsync(
        AssetReference shootReference, AssetReference deathReference)
    {
        try
        {
            _shootHandle = Addressables.LoadAssetAsync<AudioClip>(shootReference);
            await _shootHandle.ToUniTask(cancellationToken: _cts.Token);

            if (_shootHandle.Status == AsyncOperationStatus.Succeeded)
                _shootResource = _shootHandle.Result;

            _deathHandle = Addressables.LoadAssetAsync<AudioClip>(deathReference);
            await _deathHandle.ToUniTask(cancellationToken: _cts.Token);

            if (_deathHandle.Status == AsyncOperationStatus.Succeeded)
                _deathResource = _deathHandle.Result;

            return (_shootResource, _deathResource);
        }
        catch (OperationCanceledException)
        {
            return default;
        }
    }

    public void ReleaseSounds()
    {
        _cts.Cancel();
        _cts.Dispose();

        if (_shootHandle.IsValid())
            _shootHandle.Release();

        if (_deathHandle.IsValid())
            _deathHandle.Release();

        Resources.UnloadAsset(_shootResource);
        Resources.UnloadAsset(_deathResource);
    }
}
