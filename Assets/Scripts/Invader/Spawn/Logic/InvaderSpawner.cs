using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Linq;
using System.Threading;
using UnityEngine;
using VContainer.Unity;

public class InvaderSpawner : IStartable, IDisposable
{
    private readonly InvaderFactory _factory;
    private readonly InvaderRegistry _registry;
    private readonly FieldSettings _fieldSettings;
    private readonly InvaderSpawnSettings _spawnSettings;
    private readonly Subject<Unit> _started = new();
    private readonly Subject<Unit> _ended = new();
    private readonly CancellationTokenSource _cts = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public InvaderSpawner(InvaderFactory factory,
        InvaderRegistry registry,
        FieldSettings fieldSettings,
        InvaderSpawnSettings spawnSettings)
    {
        _factory = factory;
        _registry = registry;
        _fieldSettings = fieldSettings;
        _spawnSettings = spawnSettings;
    }

    public Observable<Unit> Started => _started;
    public Observable<Unit> Ended => _ended;

    public void Start()
    {
        _registry.Any
            .Where(any => !any)
            .DelayFrame(1)
            .Subscribe(_ => SpawnAsync(_cts.Token).Forget())
            .AddTo(_compositeDisposable);
    }

    public void Dispose()
    {
        _compositeDisposable.Dispose();

        _cts.Cancel();
        _cts.Dispose();
    }

    public async UniTask SpawnAsync(CancellationToken token)
    {
        _started.OnNext(Unit.Default);

        Bounds fieldBounds = _fieldSettings.Bounds;
        Vector3 invaderSize = _spawnSettings.Size;

        float invaderSlotWidth = invaderSize.x + _spawnSettings.SpacingX;
        float invaderSlotLength = invaderSize.z + _spawnSettings.SpacingZ;

        float totalWidth = (_spawnSettings.ColumnCount - 1) * invaderSlotWidth;
        float startX = fieldBounds.center.x - totalWidth / 2f;

        if (totalWidth > fieldBounds.size.x)
        {
            Debug.LogWarning("Not enough space to spawn the selected number of invader columns!");
            return;
        }

        float maxZ = fieldBounds.max.z
            - (fieldBounds.size.z * _spawnSettings.UpMarginRatioZ)
            - (invaderSize.z / 2f);
        float currentZ = maxZ;

        float spawnRangeZ = (1 - _spawnSettings.UpMarginRatioZ - _spawnSettings.DownMarginRatioZ)
            * fieldBounds.size.z;
        int rowCount = Mathf.FloorToInt(spawnRangeZ / invaderSlotLength);

        if (_spawnSettings.RowIndices.Length > rowCount)
        {
            Debug.LogWarning("Not enough space to spawn the selected number of invader rows!");
            return;
        }

        foreach (var rowIndex in _spawnSettings.RowIndices.Reverse())
        {
            float currentX = startX;

            for (int i = 0; i < _spawnSettings.ColumnCount; i++)
            {
                Vector3 spawnPosition = new(currentX, _spawnSettings.SpawnPositionY, currentZ);
                _factory.Create(rowIndex, spawnPosition);
                currentX += invaderSlotWidth;

                await UniTask.WaitForSeconds(_spawnSettings.Delay, cancellationToken: token);
            }

            currentZ -= invaderSlotLength;
        }

        _ended.OnNext(Unit.Default);
    }
}
