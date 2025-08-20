using Cysharp.Threading.Tasks;
using R3;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InvaderMover
{
    private readonly FieldView _fieldView;
    private readonly InvaderSettings _invaderSettings;
    private readonly List<Vector3> _positions = new();
    private readonly Subject<Vector3> _moved = new();
    private readonly Subject<Unit> _bottomReached = new();

    public InvaderMover(FieldView fieldView, InvaderSettings invaderSettings)
    {
        _fieldView = fieldView;
        _invaderSettings = invaderSettings;
    }

    public Observable<Vector3> Moved => _moved;
    public Observable<Unit> BottomReached => _bottomReached;

    public void AddPosition(Vector3 position) => _positions.Add(position);

    public async UniTask Move()
    {
        float invaderExtentsX = _invaderSettings.Extents.x;
        float minPositionX = _fieldView.Bounds.min.x + invaderExtentsX;
        float maxPositionX = _fieldView.Bounds.max.x - invaderExtentsX;

        float currentMinPositionX = _positions.Min(p => p.x);
        float currentMaxPositionX = _positions.Max(p => p.x);

        float minPositionZ = _fieldView.Bounds.min.z
            + _invaderSettings.DownMarginRatioZ * _fieldView.Bounds.size.z;

        float currentMinPositionZ = _positions.Min(p => p.z);

        int sign = 1;

        while (true)
        {
            switch (sign)
            {
                case 1 when currentMaxPositionX + _invaderSettings.StepRatioX <= maxPositionX:
                case -1 when currentMinPositionX - _invaderSettings.StepRatioX >= minPositionX:
                    float deltaX = sign * _invaderSettings.StepX;
                    Vector3 movementX = new(deltaX, 0f, 0f);
                    _moved.OnNext(movementX);

                    currentMinPositionX += deltaX;
                    currentMaxPositionX += deltaX;
                    break;
                default:
                    Vector3 movementZ = new(0f, 0f, -_invaderSettings.StepZ);
                    _moved.OnNext(movementZ);

                    currentMinPositionZ -= _invaderSettings.StepZ;
                    sign *= -1;
                    
                    if (currentMinPositionZ < minPositionZ)
                    {
                        _bottomReached.OnNext(Unit.Default);
                        return;
                    }

                    break;
            }

            await UniTask.WaitForSeconds(_invaderSettings.StepDelay);
        }
    }
}
