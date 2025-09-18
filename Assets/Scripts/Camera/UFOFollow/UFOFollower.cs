using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;
using Unity.Cinemachine;
using UnityEngine;
using VContainer.Unity;

public class UFOFollower : IInitializable, IDisposable
{
    private const float FarOffsetY = 35f;
    private const float NearOffsetY = 4f;
    private const float TransitionDuration = 1.5f;
    private const float ShowingDuration = 1.5f;

    private readonly UFOMover _ufoMover;
    private readonly CinemachineBrain _ufoCinemachineBrain;
    private readonly CinemachineFollow _ufoCinemachineFollow;
    private readonly CancellationTokenSource _cts = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public UFOFollower(UFOMover ufoMover,
        CinemachineBrain ufoCinemachineBrain,
        CinemachineFollow ufoCinemachineFollow)
    {
        _ufoMover = ufoMover;
        _ufoCinemachineBrain = ufoCinemachineBrain;
        _ufoCinemachineFollow = ufoCinemachineFollow;
    }

    public void Initialize()
    {
        _ufoCinemachineBrain.gameObject.SetActive(false);

        _ufoMover.Started
            .Subscribe(_ => ShowFollowCameraAsync().Forget())
            .AddTo(_compositeDisposable);
    }

    public void Dispose()
    {
        _compositeDisposable.Dispose();

        _cts.Cancel();
        _cts.Dispose();
    }

    private async UniTask ShowFollowCameraAsync()
    {
        _ufoCinemachineBrain.gameObject.SetActive(true);

        await ChangeOffsetAsync(FarOffsetY, NearOffsetY);
        await UniTask.WaitForSeconds(ShowingDuration, cancellationToken: _cts.Token);

        _ufoCinemachineBrain.gameObject.SetActive(false);
    }

    private async UniTask ChangeOffsetAsync(float start, float end)
    {
        Vector3 offset = _ufoCinemachineFollow.FollowOffset;
        float elapsed = 0f;

        while (elapsed < TransitionDuration)
        {
            offset.y = Mathf.Lerp(start, end, elapsed / TransitionDuration);
            _ufoCinemachineFollow.FollowOffset = offset;
            elapsed += Time.deltaTime;

            await UniTask.Yield(_cts.Token);
        }

        offset.y = end;
        _ufoCinemachineFollow.FollowOffset = offset;
    }
}
