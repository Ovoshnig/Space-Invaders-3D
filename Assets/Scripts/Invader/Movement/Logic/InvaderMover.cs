using Cysharp.Threading.Tasks;
using R3;
using System.Linq;
using System.Threading;
using UnityEngine;

public class InvaderMover
{
    private readonly FieldView _fieldView;
    private readonly InvaderSpawnSettings _invaderSpawnSettings;
    private readonly InvaderMovementSettings _invaderMovementSettings;
    private readonly Subject<Vector3> _moved = new();
    private readonly Subject<Unit> _bottomReached = new();

    private float _delay = 1f;
    private float _currentMinPositionX = 0f;
    private float _currentMaxPositionX = 0f;
    private float _currentMinPositionZ = 0f;
    private bool _isPause = false;

    public InvaderMover(FieldView fieldView,
        InvaderSpawnSettings invaderSpawnSettings,
        InvaderMovementSettings invaderMovementSettings)
    {
        _fieldView = fieldView;
        _invaderSpawnSettings = invaderSpawnSettings;
        _invaderMovementSettings = invaderMovementSettings;
    }

    public Observable<Vector3> Moved => _moved;
    public Observable<Unit> BottomReached => _bottomReached;

    public void SetPositions(Vector3[] positions)
    {
        if (!positions.Any())
            return;

        CalculateDelay(positions);

        _currentMinPositionX = positions.Min(p => p.x);
        _currentMaxPositionX = positions.Max(p => p.x);
        _currentMinPositionZ = positions.Min(p => p.z);
    }

    public async UniTask StartMovingAsync(CancellationToken token)
    {
        float invaderExtentsX = _invaderSpawnSettings.Extents.x;
        float minPositionX = _fieldView.Bounds.min.x + invaderExtentsX;
        float maxPositionX = _fieldView.Bounds.max.x - invaderExtentsX;
        float minPositionZ = _fieldView.Bounds.min.z
            + _invaderSpawnSettings.DownMarginRatioZ * _fieldView.Bounds.size.z;

        int sign = 1;

        while (true)
        {
            switch (sign)
            {
                case 1 when _currentMaxPositionX + _invaderMovementSettings.StepRatioX <= maxPositionX:
                case -1 when _currentMinPositionX - _invaderMovementSettings.StepRatioX >= minPositionX:
                    float deltaX = sign * _invaderMovementSettings.StepX;
                    Vector3 movementX = new(deltaX, 0f, 0f);
                    _moved.OnNext(movementX);

                    _currentMinPositionX += deltaX;
                    _currentMaxPositionX += deltaX;
                    break;
                default:
                    Vector3 movementZ = new(0f, 0f, -_invaderMovementSettings.StepZ);
                    _moved.OnNext(movementZ);

                    _currentMinPositionZ -= _invaderMovementSettings.StepZ;
                    sign *= -1;

                    if (_currentMinPositionZ < minPositionZ)
                    {
                        _bottomReached.OnNext(Unit.Default);
                        return;
                    }

                    break;
            }

            await UniTask.WaitForSeconds(_delay, cancellationToken: token);

            if (_isPause)
                await UniTask.WaitWhile(() => _isPause, cancellationToken: token);
        }
    }

    public void SetPause(bool value) => _isPause = value;

    private void CalculateDelay(Vector3[] positions)
    {
        float percentageLeft = (float)positions.Length / _invaderSpawnSettings.InitialCount;

        _delay = Mathf.Lerp(_invaderMovementSettings.EndDelay,
            _invaderMovementSettings.StartDelay,
            percentageLeft);
    }
}
