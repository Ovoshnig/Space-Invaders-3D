using Cysharp.Threading.Tasks;
using R3;
using System.Linq;
using System.Threading;
using UnityEngine;

public class InvaderMover
{
    private readonly FieldView _fieldView;
    private readonly InvaderSettings _invaderSettings;
    private readonly Subject<Vector3> _moved = new();
    private readonly Subject<Unit> _bottomReached = new();

    private float _currentMinPositionX = 0f;
    private float _currentMaxPositionX = 0f;
    private float _currentMinPositionZ = 0f;

    public InvaderMover(FieldView fieldView, InvaderSettings invaderSettings)
    {
        _fieldView = fieldView;
        _invaderSettings = invaderSettings;
    }

    public Observable<Vector3> Moved => _moved;
    public Observable<Unit> BottomReached => _bottomReached;

    public void SetPositions(Vector3[] positions)
    {
        if (!positions.Any())
            return;

        _currentMinPositionX = positions.Min(p => p.x);
        _currentMaxPositionX = positions.Max(p => p.x);
        _currentMinPositionZ = positions.Min(p => p.z);
    }

    public async UniTask Move(CancellationToken token)
    {
        float invaderExtentsX = _invaderSettings.Extents.x;
        float minPositionX = _fieldView.Bounds.min.x + invaderExtentsX;
        float maxPositionX = _fieldView.Bounds.max.x - invaderExtentsX;
        float minPositionZ = _fieldView.Bounds.min.z
            + _invaderSettings.DownMarginRatioZ * _fieldView.Bounds.size.z;

        int sign = 1;

        while (true)
        {
            switch (sign)
            {
                case 1 when _currentMaxPositionX + _invaderSettings.StepRatioX <= maxPositionX:
                case -1 when _currentMinPositionX - _invaderSettings.StepRatioX >= minPositionX:
                    float deltaX = sign * _invaderSettings.StepX;
                    Vector3 movementX = new(deltaX, 0f, 0f);
                    _moved.OnNext(movementX);

                    _currentMinPositionX += deltaX;
                    _currentMaxPositionX += deltaX;
                    break;
                default:
                    Vector3 movementZ = new(0f, 0f, -_invaderSettings.StepZ);
                    _moved.OnNext(movementZ);

                    _currentMinPositionZ -= _invaderSettings.StepZ;
                    sign *= -1;

                    if (_currentMinPositionZ < minPositionZ)
                    {
                        _bottomReached.OnNext(Unit.Default);
                        return;
                    }

                    break;
            }

            await UniTask.WaitForSeconds(_invaderSettings.StepDelay, cancellationToken: token);
        }
    }
}
