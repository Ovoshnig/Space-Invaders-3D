using Cysharp.Threading.Tasks;
using R3;
using System;
using UnityEngine;
using VContainer.Unity;

public class UFOMover : IInitializable, IDisposable
{
    private readonly PlayerShooterModel _playerShooterModel;
    private readonly FieldView _fieldView;
    private readonly UFOMovementSettings _ufoMovementSettings;
    private readonly InvaderSpawnSettings _invaderSpawnSettings;
    private readonly Subject<Vector3> _started = new();
    private readonly Subject<Unit> _ended = new();
    private readonly Subject<Vector3> _moved = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    private Vector3 _leftStartPosition;
    private Vector3 _rightStartPosition;
    private int _direction = 1;
    private bool _isMoving = false;

    public UFOMover(PlayerShooterModel playerShooterModel,
        FieldView fieldView,
        UFOMovementSettings ufoMovementSettings,
        InvaderSpawnSettings invaderSpawnSettings)
    {
        _playerShooterModel = playerShooterModel;
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
            .Subscribe(count => OnShotCountChanged(count).Forget())
            .AddTo(_compositeDisposable);
    }

    public void Dispose() => _compositeDisposable.Dispose();

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

    private async UniTask OnShotCountChanged(int count)
    {
        if (_isMoving || !ShouldSpawnUFO(count))
            return;

        _isMoving = true;

        Vector3 startPosition = _direction == 1 ? _leftStartPosition : _rightStartPosition;
        _started.OnNext(startPosition);
        await MoveAsync(startPosition);
        _ended.OnNext(Unit.Default);

        _direction *= -1;
        _isMoving = false;
    }

    private async UniTask MoveAsync(Vector3 startPosition)
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

            await UniTask.Yield();
        }
    }
}
