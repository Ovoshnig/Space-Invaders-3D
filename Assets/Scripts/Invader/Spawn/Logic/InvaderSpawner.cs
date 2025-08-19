using R3;
using System.Linq;
using UnityEngine;

public class InvaderSpawner
{
    private readonly FieldView _fieldView;
    private readonly InvaderSettings _invaderSettings;
    private readonly Subject<(int index, Vector3 position)> _positionSelected = new();

    public InvaderSpawner(FieldView fieldView, InvaderSettings invaderSettings)
    {
        _fieldView = fieldView;
        _invaderSettings = invaderSettings;
    }

    public Observable<(int index, Vector3 position)> PositionSelected => _positionSelected;

    public void StartSpawn()
    {
        Bounds fieldBounds = _fieldView.Bounds;
        Vector3 invaderSize = _invaderSettings.InvaderBounds.size;

        float invaderSlotWidth = invaderSize.x + (_invaderSettings.SpacingRatioX * invaderSize.x);
        float invaderSlotDepth = invaderSize.z + (_invaderSettings.SpacingRatioZ * invaderSize.z);

        float spawnRangeX = fieldBounds.size.x * _invaderSettings.SpawnRangeRatioX;
        int columnCount = Mathf.FloorToInt(spawnRangeX / invaderSlotWidth);

        if (columnCount == 0)
        {
            Debug.LogWarning("Недостаточно места для спауна даже одной колонки захватчиков!");
            return;
        }

        float totalWidth = (columnCount - 1) * invaderSlotWidth;
        float startX = fieldBounds.center.x - totalWidth / 2f;

        float startZ = fieldBounds.max.z
            - (fieldBounds.size.z * _invaderSettings.MarginRatioZ)
            - (invaderSize.z / 2f);
        float currentZ = startZ;

        foreach (var rowIndex in _invaderSettings.RowIndices.Reverse())
        {
            float currentX = startX;

            for (int i = 0; i < columnCount; i++)
            {
                Vector3 spawnPosition = new(currentX, _invaderSettings.SpawnPositionY, currentZ);
                _positionSelected.OnNext((rowIndex, spawnPosition));
                currentX += invaderSlotWidth;
            }

            currentZ -= invaderSlotDepth;
        }
    }
}
