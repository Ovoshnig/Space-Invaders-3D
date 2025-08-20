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
        Vector3 invaderSize = _invaderSettings.Size;

        float invaderSlotWidth = invaderSize.x + _invaderSettings.SpacingX;
        float invaderSlotLength = invaderSize.z + _invaderSettings.SpacingZ;

        float spawnRangeX = fieldBounds.size.x * _invaderSettings.SpawnRangeRatioX;
        int columnCount = Mathf.FloorToInt(spawnRangeX / invaderSlotWidth);

        if (columnCount == 0)
        {
            Debug.LogWarning("Недостаточно места для спауна даже одной колонки захватчиков!");
            return;
        }

        float totalWidth = (columnCount - 1) * invaderSlotWidth;
        float startX = fieldBounds.center.x - totalWidth / 2f;

        float maxZ = fieldBounds.max.z
            - (fieldBounds.size.z * _invaderSettings.UpMarginRatioZ)
            - (invaderSize.z / 2f);
        float currentZ = maxZ;

        float spawnRangeZ = (1 - _invaderSettings.UpMarginRatioZ - _invaderSettings.DownMarginRatioZ)
            * fieldBounds.size.z;
        int rowCount = Mathf.FloorToInt(spawnRangeZ / invaderSlotLength);

        if (_invaderSettings.RowIndices.Length > rowCount)
        {
            Debug.LogWarning("Недостаточно места для спауна "
                + "выбранного количества строк захватчиков!");
            return;
        }

        foreach (var rowIndex in _invaderSettings.RowIndices.Reverse())
        {
            float currentX = startX;

            for (int i = 0; i < columnCount; i++)
            {
                Vector3 spawnPosition = new(currentX, _invaderSettings.SpawnPositionY, currentZ);
                _positionSelected.OnNext((rowIndex, spawnPosition));
                currentX += invaderSlotWidth;
            }

            currentZ -= invaderSlotLength;
        }
    }
}
