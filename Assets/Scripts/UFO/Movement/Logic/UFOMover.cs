using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;
using UnityEngine;
using VContainer.Unity;

public class UFOMover : IInitializable, IDisposable
{
    private readonly PlayerShooterModel _playerShooterModel;
    private readonly UFODestroyer _ufoDestroyer;
    private readonly InvaderRegistry _invaderRegistry;
    private readonly FieldView _fieldView;
    private readonly UFOMovementSettings _ufoMovementSettings;
    private readonly InvaderSpawnSettings _invaderSpawnSettings;
    private readonly Subject<Vector3> _started = new();
    private readonly Subject<Unit> _ended = new();
    private readonly Subject<Vector3> _moved = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    private CancellationTokenSource _cts = new();
    private Vector3 _leftStartPosition;
    private Vector3 _rightStartPosition;
    private int _direction = 1;
    private bool _isMoving = false;
    private bool _isPause = false;

    public UFOMover(PlayerShooterModel playerShooterModel,
        UFODestroyer ufoDestroyer,
        InvaderRegistry invaderRegistry,
        FieldView fieldView,
        UFOMovementSettings ufoMovementSettings,
        InvaderSpawnSettings invaderSpawnSettings)
    {
        _playerShooterModel = playerShooterModel;
        _ufoDestroyer = ufoDestroyer;
        _invaderRegistry = invaderRegistry;
        _fieldView = fieldView;
        _ufoMovementSettings = ufoMovementSettings;
        _invaderSpawnSettings = invaderSpawnSettings;
    }

    public Observable<Vector3> Started => _started;
    public Observable<Unit> Ended => _ended;
    public Observable<Vector3> Moved => _moved;

    public void Initialize()
    {
        Bounds fieldBounds = _fieldView.Bounds;

        float fieldMinX = fieldBounds.min.x;
        float fieldMaxX = fieldBounds.max.x;
        float ufoExtentsX = _ufoMovementSettings.Extents.x;
        float leftStartPositionX = fieldMinX - ufoExtentsX;
        float rightStartPositionX = fieldMaxX + ufoExtentsX;

        float startPositionY = fieldBounds.min.y;

        float startPositionZ = fieldBounds.max.z
            - 0.5f * (fieldBounds.size.z * _invaderSpawnSettings.UpMarginRatioZ);

        _leftStartPosition = new Vector3(leftStartPositionX, startPositionY, startPositionZ);
        _rightStartPosition = new Vector3(rightStartPositionX, startPositionY, startPositionZ);

        _playerShooterModel.ShotCount
            .Subscribe(OnShotCountChanged)
            .AddTo(_compositeDisposable);

        _ufoDestroyer.Destroyed
            .Subscribe(_ => OnDestroyed())
            .AddTo(_compositeDisposable);

        _invaderRegistry.Any
            .Where(any => !any)
            .Subscribe(_ => OnInvadersEmpty())
            .AddTo(_compositeDisposable);
    }

    public void Dispose()
    {
        CancelCTS();

        _compositeDisposable.Dispose();
    }

    public void SetPause(bool value) => _isPause = value;

    private bool ShouldSpawnUFO(int currentShotCount)
    {
        if (currentShotCount == _ufoMovementSettings.FirstShotCount)
            return true;

        if (currentShotCount > _ufoMovementSettings.FirstShotCount)
        {
            int difference = currentShotCount - _ufoMovementSettings.FirstShotCount;
            return difference % _ufoMovementSettings.NextShotCount == 0;
        }

        return false;
    }

    private void OnShotCountChanged(int count)
    {
        if (_isMoving || !ShouldSpawnUFO(count))
            return;

        StartMoveAsync().Forget();
    }

    private void OnDestroyed() => ResetCTS();

    private void OnInvadersEmpty() => ResetCTS();

    private async UniTask StartMoveAsync()
    {
        Vector3 startPosition = _direction == 1 ? _leftStartPosition : _rightStartPosition;
        _isMoving = true;
        _started.OnNext(startPosition);

        try
        {
            await MoveAsync(startPosition, _cts.Token);
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            _direction *= -1;
            _isMoving = false;
            _ended.OnNext(Unit.Default);
        }
    }

    private async UniTask MoveAsync(Vector3 startPosition, CancellationToken token)
    {
        float currentPositionX = startPosition.x;
        float targetX = _direction == 1 ? _rightStartPosition.x : _leftStartPosition.x;
        Vector3 movement = Vector3.zero;

        while (_direction * currentPositionX < _direction * targetX)
        {
            float deltaX = _direction * Time.deltaTime * _ufoMovementSettings.Speed;
            currentPositionX += deltaX;
            movement.x = deltaX;
            _moved.OnNext(movement);

            await UniTask.Yield(cancellationToken: token);

            if (_isPause)
                await UniTask.WaitWhile(() => _isPause, cancellationToken: token);
        }
    }

    private void CancelCTS()
    {
        _cts.Cancel();
        _cts.Dispose();
    }

    private void ResetCTS()
    {
        CancelCTS();
        _cts = new CancellationTokenSource();
    }
}
